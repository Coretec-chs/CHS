using System.Web.Mvc;
using System.Web.UI;
using Core;
using Core.Model;
using Core.Service;
using WebUI.Mappers;
using WebUI.ViewModels.Inputs;
using System.Security.Claims;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    /// <summary>
    /// generic crud controller for entities where there is difference between the edit and create view;
    /// used to do crud with both the grid and the ajaxlist
    /// </summary>
    /// <typeparam name="TEntity"> the entity</typeparam>
    /// <typeparam name="TCreateInput">create viewmodel</typeparam>
    /// <typeparam name="TEditInput">edit viewmodel</typeparam>
    public abstract class NavCrudere<TEntity, TCreateInput, TEditInput> : Controller
        where TCreateInput : Input, new()
        where TEditInput : Input, new()
        where TEntity : class, new()
    {
        protected readonly INavService navService;

        protected readonly IProMapper mapper;



        public NavCrudere(INavService navService, IProMapper mapper)
        {
            this.navService = navService;
            this.mapper = mapper;
        }

        protected virtual string EditView
        {
            get { return "edit"; }
        }

        public virtual Task<object[]> LogicBeforeCreate(TCreateInput input, bool diplayMessage = false)
        {
            return Task.FromResult(new Object[] { diplayMessage, string.Empty });
        }

        public virtual Task<TEntity> LogicAfterCreate(TEntity entity, TCreateInput input)
        {
            return Task.FromResult(entity);
        }

        public virtual Task<object[]> LogicAfterCreate(TEntity entity, bool diplayMessage = false)
        {
            return Task.FromResult(new Object[] { diplayMessage, string.Empty });
        }

        public virtual Task<object[]> LogicBeforeEdit(TEditInput input, bool diplayMessage = false)
        {
            return Task.FromResult(new Object[] { diplayMessage, string.Empty });
        }

        public virtual Task<TEntity> LogicAfterEdit(TEntity entity, TEditInput input)
        {
            return Task.FromResult(entity);
        }

        public virtual Task<object[]> LogicAfterEdit(TEntity entity, bool diplayMessage = false, string action = null)
        {
            return Task.FromResult(new Object[] { diplayMessage, string.Empty });
        }

        public virtual TCreateInput DefaultValuesCreateGet(TCreateInput input, object searchValue = null)
        {
            return input;
        }

        public virtual TEditInput DefaultValuesEditGet(TEditInput input)
        {
            return input;
        }

        public virtual TCreateInput DefaultValuesCreatePost(TCreateInput input)
        {
            return input;
        }

        public virtual TEditInput DefaultValuesEditPost(TEditInput input)
        {
            return input;
        }

        public abstract string EntityKeys(TEntity entity);


        public abstract TEntity FindEntity(object[] keyValue);


        public abstract TEntity FindEntity(TEditInput input);




        //public virtual Task<ActionResult> Index(object filterValue = null)
        //{
        //    //return View();
        //}

        public virtual ActionResult Create(string searchValue = null)
        {
            try
            {
                var entity = mapper.Map<TEntity, TCreateInput>(new TEntity());
                entity = DefaultValuesCreateGet(entity, searchValue);
                return View(entity);
            }
            catch (CustomException ex)
            {
                ViewBag.Message = "Error, Error Occurred! " + ex.Message + ",error";
                var entity = mapper.Map<TEntity, TCreateInput>(new TEntity());
                return View(entity);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error, Error Occurred! " + ex.Message + ",error";
                var entity = mapper.Map<TEntity, TCreateInput>(new TEntity());
                return View(entity);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create(TCreateInput input)
        {
            try
            {
                //ModelState.Clear();

                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    ViewBag.Message = "Error," + messages + ",error";
                    return View(input);
                }

                input = DefaultValuesCreatePost(input);
                await LogicBeforeCreate(input);
                var item = mapper.Map<TCreateInput, TEntity>(input);
                var entity = await LogicAfterCreate(item, input);

                entity = await navService.CreateAsync<TEntity>(item);
                await navService.SaveAsync();
                var displaymsg = "Saved Successfully!!";
                var keyVals = EntityKeys(entity);
                try
                {
                    var logicObj = await LogicAfterCreate(entity, false);

                    if (Convert.ToBoolean(logicObj[0]))
                    {
                        displaymsg = "Saved Successfully!! " + logicObj[1].ToString();
                    }

                }
                catch (CustomException ex)
                {
                    displaymsg = "Saved Successfully!! " + ex.Message;
                }
                catch (Exception ex)
                {
                    displaymsg = "Saved Successfully!! " + ex.Message;
                }
                TempData["Message"] = "Saving," + displaymsg + ",success";
                return RedirectToAction("Edit", new { id = keyVals });
            }
            catch (CustomException ex)
            {
                ViewBag.action = "view";
                ViewBag.Message = "Error, Error Occurred! " + ex.Message + ",error";
                input = DefaultValuesCreateGet(input, "");
                return View(input);
            }
            catch (Exception ex)
            {
                ViewBag.action = "view";
                ViewBag.Message = "Error, Error Occurred! " + ex.Message + ",error";
                input = DefaultValuesCreateGet(input, "");
                return View(input);
            }
        }

        [OutputCache(Location = OutputCacheLocation.None)]//for ie
        public virtual ActionResult Edit(string id)
        {
            try
            {
                var values = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var entity = FindEntity(values);
                var model = mapper.Map<TEntity, TEditInput>(entity);
                model = DefaultValuesEditGet(model);
                return View(EditView, model);
            }
            catch (CustomException ex)
            {
                ViewBag.Message = "Error, Error Occurred! " + ex.Message + ",error";
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error, Error Occurred! " + ex.Message + ",error";
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Edit(TEditInput input, string action = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    ViewBag.Message = "Error," + messages + ",error";
                    return View(EditView, input);
                }
                input = DefaultValuesEditPost(input);
                await LogicBeforeEdit(input);
                var entity = FindEntity(input);
                entity = mapper.Map<TEditInput, TEntity>(input, entity);
                entity = await LogicAfterEdit(entity, input);
                entity = await navService.UpdateAsync<TEntity>(entity);
                await navService.SaveAsync();
                var displaymsg = "Saved Successfully!!";
                input = mapper.Map<TEntity, TEditInput>(entity);
                input = DefaultValuesEditGet(input);
                try
                {
                    var logicObj = await LogicAfterEdit(entity, false, action);
                    if (Convert.ToBoolean(logicObj[0]))
                    {
                        displaymsg = "Saved Successfully! " + logicObj[1].ToString();
                    }
                }
                catch (CustomException ex)
                {
                    displaymsg = "Saved Successfully!! " + ex.Message;
                }
                catch (Exception ex)
                {
                    displaymsg = "Saved Successfully!! " + ex.Message;
                }
                ViewBag.Message = "Saving," + displaymsg + ",success";
                return View(EditView, input);
            }
            catch (CustomException ex)
            {

                ViewBag.Message = "Error,Error! " + ex.Message + ",error";
                input = DefaultValuesEditGet(input);
                return View(EditView, input);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error, Error Occurred! " + ex.Message + ",error";
                input = DefaultValuesEditGet(input);
                return View(EditView, input);
            }
        }

        public ActionResult Delete(params object[] keyValues)
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Delete(DeleteConfirmInput input)
        //{
        //    navService.Delete(input);
        //    return Json(new { input.Id });
        //}

        /// <summary>
        /// used by the AjaxList to render an item, also Create,Edit and Restore actions use it to render an item and return it to the ajaxlist so it would be updated
        /// </summary>
        protected virtual string RowViewName
        {
            get
            {
                return string.Empty;
            }
        }
    }
}