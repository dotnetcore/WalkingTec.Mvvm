﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Demo.Models.Virus;
using WalkingTec.Mvvm.Demo.Models;


namespace WalkingTec.Mvvm.BlazorDemo.ViewModel.VirusData.PatientVMs
{
    public partial class PatientVM : BaseCRUDVM<Patient>
    {
        [Display(Name = "病毒")]
        public List<string> SelectedVirusesIDs { get; set; }

        public PatientVM()
        {
            SetInclude(x => x.Location);
            SetInclude(x => x.Hospital);
            SetInclude(x => x.Viruses);
        }

        protected override void InitVM()
        {
            SelectedVirusesIDs = Entity.Viruses?.Select(x => x.VirusId.ToString()).ToList();
        }

        public override void DoAdd()
        {
            Entity.Viruses = new List<PatientVirus>();
            if (SelectedVirusesIDs != null)
            {
                foreach (var id in SelectedVirusesIDs)
                {
                     PatientVirus middle = new PatientVirus();
                    middle.SetPropertyValue("VirusId", id);
                    Entity.Viruses.Add(middle);
                }
            }
           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            Entity.Viruses = new List<PatientVirus>();
            if(SelectedVirusesIDs != null )
            {
                 foreach (var item in SelectedVirusesIDs)
                {
                    PatientVirus middle = new PatientVirus();
                    middle.SetPropertyValue("VirusId", item);
                    Entity.Viruses.Add(middle);
                }
            }

            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
