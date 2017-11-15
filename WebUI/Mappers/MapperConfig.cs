using System;

using Core.Model;
using WebUI.Mappers.Injections;
using WebUI.ViewModels.Inputs;
using Omu.ValueInjecter;

namespace WebUI.Mappers
{
    public class MapperConfig
    {
        public static MapperInstance CrudMapper = new MapperInstance();

        public static void Configure()
        {
            // tag is being used here to pass in the existing Entity ( pulled from the Db )
            CrudMapper.DefaultMap = (src, resType, tag) =>
                {
                    var res = tag != null && tag.GetType().IsSubclassOf(typeof(Entity)) ? tag : Activator.CreateInstance(resType);

                    // handle basic properties
                    res.InjectFrom(src);
                    var srcType = src.GetType();

                    // handle the rest Country.Id <-> CountryId; int <-> int?
                    if (srcType.IsSubclassOf(typeof(Entity)) && resType.IsSubclassOf(typeof(Input)))
                    {
                        res.InjectFrom<NormalToNullables>(src)
                           .InjectFrom<EntitiesToInts>(src);
                    }
                    else if (srcType.IsSubclassOf(typeof(Input)) && resType.IsSubclassOf(typeof(Entity)))
                    {
                        res.InjectFrom<IntsToEntities>(src).
                          InjectFrom<NullablesToNormal>(src);
                    }
                    else if (!srcType.IsSubclassOf(typeof(Entity)) && resType.IsSubclassOf(typeof(Input)))
                    {
                        res.InjectFrom<NormalToNullables>(src);
                        //.InjectFrom<EntitiesToInts>(src);
                    }
                    else if (srcType.IsSubclassOf(typeof(Input)) && !resType.IsSubclassOf(typeof(Entity)))
                    {
                        //res.InjectFrom<IntsToEntities>(src)
                        res.InjectFrom<NullablesToNormal>(src);
                    }

                    return res;
                };

        }
    }
}