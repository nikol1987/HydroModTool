using HydroneerStager.WinForms.Data;
using ReactiveUI;
using System;
using System.Collections.Generic;
using static HydroneerStager.WinForms.Views.ApplicationTabs.ProjectTabs.ProjectsView;

namespace HydroneerStager.WinForms.Converters
{
    public sealed class ProjectsConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            /*if (toType == typeof(IReadOnlyCollection<ProjectListView>) && fromType == typeof(IReadOnlyCollection<ProjectModel>))
            {
                return 100;
            }*/

            return 0;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            /*var srcObj = from as IReadOnlyCollection<ProjectModel>;
            var returnObj = new List<ProjectListView>();

            if (from == null)
            {
                result = returnObj;

                return true;
            }

            foreach (var project in srcObj)
            {
                returnObj.Add(new ProjectListView(project.Id, project.Name));
            }

            result = returnObj;*/
            result = null;
            return false;
        }
    }
}
