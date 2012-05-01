﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BLL.Model.Entity;

namespace CodeFirst
{
    internal class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<SmartAccountContext>
    {
        protected override void Seed(SmartAccountContext context)
        {
            List<Head> heads = new List<Head>
                                   {
                                       new Head
                                           {
                                               Name = "Bank Book",
                                               IsActive = true,
                                               Type = "Capital",
                                               Description = "Default Bank Book"
                                           },
                                       new Head
                                           {
                                               Name = "Cash Book",
                                               IsActive = true,
                                               Type = "Capital",
                                               Description = "Default Cash Book"
                                           }
                                   };

            foreach (Head head in heads)
            {
                context.Heads.Add(head);
            }

            List<Parameter> parameters = new List<Parameter>
                                             {
                                                 new Parameter
                                                     {
                                                         Key = "FinancialYearStartDate",
                                                         Value = DateTime.Now.ToString(),
                                                         IsActive = true
                                                     }
                                             };

            foreach (Parameter parameter in parameters)
            {
                context.Parameters.Add(parameter);
            }

            context.SaveChanges();
        }
    }
}
