using System;
using System.Collections.Generic;

namespace SiteManager.Repository.Mapper
{
    public interface IMapper<TFirst, TSecond> : IMapperBase
    {
        TFirst Map(TSecond element);
        TSecond Map(TFirst element);
        List<TFirst> Map(List<TSecond> elements, Action<TFirst> callback = null);
        List<TSecond> Map(List<TFirst> elements, Action<TSecond> callback = null);
    }
}