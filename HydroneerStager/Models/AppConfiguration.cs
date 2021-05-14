using HydroneerStager.Models;
using System;
using System.Collections.Generic;

namespace HydroneerStager
{
    public class AppConfiguration
    {
        public List<Project> Projects { get; set; } = new List<Project>();

        public Guid? DefaultProject { get; set; } = null;

        public List<GuidItem> Guids { get; set; } = new List<GuidItem>();
    }
}