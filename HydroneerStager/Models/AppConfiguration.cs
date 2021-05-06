using HydroneerStager.Models;
using System;
using System.Collections.Generic;

namespace HydroneerStager
{
    public class AppConfiguration
    {
        public IList<Project> Projects { get; set; } = new List<Project>();

        public Guid? DefaultProject { get; set; } = null;

        public IList<GuidItem> Guids { get; set; } = new List<GuidItem>();
    }
}