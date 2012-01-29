using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public static class ExtensionsHelper
    {
        public static List<TDTO> ToDTOList<TDTO, TEntity>(this EntityCollection<TEntity> collection)
            where TEntity : class, IDTOable<TDTO>
            where TDTO : BaseEntityDTO
        {
            List<TDTO> result = new List<TDTO>();

            foreach (TEntity item in collection)
                result.Add(item.ToDTO());

            return result;
        }

        public static void Update<TEntity, TDTO>(this EntityCollection<TEntity> collection, List<TDTO> list)
            where TEntity : class, IDataBase, IDTOable<TDTO>, new()
            where TDTO : BaseEntityDTO
        {
            foreach (TDTO item in list)
            {
                TEntity entity = null;
                if (item.ID > 0)
                {
                    entity = collection.FirstOrDefault(e => e.ID == item.ID);
                    entity.Assign(item);
                }
                else
                {
                    entity = new TEntity();
                    entity.Assign(item);
                    collection.Add(entity);
                }
            }
        }
    }
}
