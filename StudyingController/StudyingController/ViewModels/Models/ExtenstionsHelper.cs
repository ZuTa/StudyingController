using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels.Models
{
    public static class ExtenstionsHelper
    {
        public static List<TDTO> ToDTOList<TDTO, TModel>(this ObservableCollection<TModel> collection)
            where TModel : BaseModel, IDTOable<TDTO>
            where TDTO : BaseEntityDTO
        {
            List<TDTO> result = new List<TDTO>();

            foreach (TModel item in collection)
                result.Add(item.ToDTO());

            return result;
        }

        public static ObservableCollection<TModel> ToModelList<TModel, TDTO>(this List<TDTO> list)
            where TModel : BaseModel, IDTOable<TDTO>, new()
            where TDTO : BaseEntityDTO
        {
            ObservableCollection<TModel> result = new ObservableCollection<TModel>();

            foreach (TDTO item in list)
            {
                TModel model = new TModel();
                model.Assign(item);
                result.Add(model);
            }

            return result;
        }

    }
}
