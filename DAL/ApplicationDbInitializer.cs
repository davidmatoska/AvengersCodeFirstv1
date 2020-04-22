using Avengers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.DAL
{
    public class ApplicationDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            // David: déclaration des variables


            var civils = new List<Civil> { };
            civils.ForEach(s => context.Civils.Add(s));
            context.SaveChanges();

            var organisations = new List<Organisation> { };
            organisations.ForEach(s => context.Organisations.Add(s));
            context.SaveChanges();

            var pays = new List<Pays> 
            {
                 new Pays{PaysID=1,Pays_nom ="France",},
                 new Pays{PaysID=2,Pays_nom ="Italie",},
                 new Pays{PaysID=3,Pays_nom ="USA",},
                 new Pays{PaysID=4,Pays_nom ="Russie",}
            };
            pays.ForEach(s => context.Pays.Add(s));
            context.SaveChanges();

            var heros = new List<Heros> { };
            heros.ForEach(s => context.Heros.Add(s));
            context.SaveChanges();

            var mechants = new List<Mechant> { };
            mechants.ForEach(s => context.Mechants.Add(s));
            context.SaveChanges();

            var incidents = new List<Incident> { };
            incidents.ForEach(s => context.Incidents.Add(s));
            context.SaveChanges();

            var incident_motif = new List<Incident_Motif> 
            {
             new Incident_Motif{Incident_MotifID=1,Motif ="Accident Grave"},
             new Incident_Motif{Incident_MotifID=2,Motif ="Violences"},
             new Incident_Motif{Incident_MotifID=3,Motif ="Catastrophe Naturelle"},
             new Incident_Motif{Incident_MotifID=4,Motif ="Créatures Inconnues"},
             new Incident_Motif{Incident_MotifID=5,Motif ="Super Méchant"},
             new Incident_Motif{Incident_MotifID=6,Motif ="Autre"}
            };
            incident_motif.ForEach(s => context.Incident_Motifs.Add(s));
            context.SaveChanges();

            var missions = new List<Mission> { };
            missions.ForEach(s => context.Missions.Add(s));
            context.SaveChanges();

            var satisfactions = new List<Satisfaction> { };
            satisfactions.ForEach(s => context.Satisfactions.Add(s));
            context.SaveChanges();

            var litiges = new List<Litige> { };
            litiges.ForEach(s => context.Litiges.Add(s));
            context.SaveChanges();









        }
    }
}