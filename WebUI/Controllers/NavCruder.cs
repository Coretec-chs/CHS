using Core.Model;
using Core.Service;
using WebUI.Mappers;
using WebUI.ViewModels.Inputs;

namespace WebUI.Controllers
{
    /// <summary>
    /// generic crud controller for entities where there is no difference between the edit and create view
    /// </summary>
    /// <typeparam name="TEntity">the entity</typeparam>
    /// <typeparam name="TInput"> viewmodel </typeparam>
    public abstract class NavCruder<TEntity, TInput> : NavCrudere<TEntity,TInput,TInput>
        where TInput : Input, new()
        where TEntity : class, new()
    {
        public NavCruder(INavService navService, IProMapper mapper) : base(navService, mapper)
        {
        }
        
        protected override string EditView
        {
            get { return "create"; }
        }
        
    }
}