using System;
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
            foreach (Parameter parameter in _parameters)
            {
                context.Parameters.Add(parameter);
            }

            foreach (Head head in _heads)
            {
                context.Heads.Add(head);
            }

            foreach (Project project in _projects)
            {
                context.Projects.Add(project);
                context.ProjectHeads.Add(new ProjectHead { Project = project, Head = _heads[0], IsActive = true });
                context.ProjectHeads.Add(new ProjectHead { Project = project, Head = _heads[1], IsActive = true });
            }

            context.ProjectHeads.Add(new ProjectHead { Project = _projects[0], Head = _heads[4], IsActive = true });
            context.ProjectHeads.Add(new ProjectHead { Project = _projects[0], Head = _heads[5], IsActive = true });
            context.ProjectHeads.Add(new ProjectHead { Project = _projects[1], Head = _heads[4], IsActive = true });
            context.ProjectHeads.Add(new ProjectHead { Project = _projects[2], Head = _heads[3], IsActive = true });

            context.ProjectHeads.Add(new ProjectHead { Project = _projects[2], Head = _heads[4], IsActive = true });
            context.ProjectHeads.Add(new ProjectHead { Project = _projects[3], Head = _heads[4], IsActive = true });
            context.ProjectHeads.Add(new ProjectHead { Project = _projects[3], Head = _heads[5], IsActive = true });

            context.SaveChanges();
        }

        private readonly List<Parameter> _parameters = new List<Parameter>
                                                           {
                                                               new Parameter
                                                                   {
                                                                       Key = "FinancialYearStartDate",
                                                                       Value = DateTime.Now.ToString("dd/mm"),
                                                                       IsActive = true
                                                                   },
                                                               new Parameter
                                                                   {
                                                                       Key = "CurrentFinancialYear",
                                                                       Value = DateTime.Now.Year.ToString(),
                                                                       IsActive = true
                                                                   },
                                                               new Parameter
                                                                   {
                                                                       Key = "CurrentFinancialYearStartDate",
                                                                       Value = DateTime.Now.ToString(),
                                                                       IsActive = true
                                                                   },
                                                               new Parameter
                                                                   {
                                                                       Key = "BankAccountNumber",
                                                                       Value = "",
                                                                       IsActive = true
                                                                   },
                                                               new Parameter
                                                                   {
                                                                       Key = "BankName",
                                                                       Value = "",
                                                                       IsActive = true
                                                                   }
                                                           };

        private readonly IList<Project> _projects = new List<Project>
                                                        {
                                                            new Project
                                                                {
                                                                    Name = "P1",
                                                                    CreateDate = DateTime.Now,
                                                                    IsActive = true,
                                                                    Description = "this is p1"
                                                                },
                                                            new Project
                                                                {
                                                                    Name = "P2",
                                                                    CreateDate = DateTime.Now,
                                                                    IsActive = true,
                                                                    Description = "this is p2"
                                                                },
                                                            new Project
                                                                {
                                                                    Name = "P3",
                                                                    CreateDate = DateTime.Now,
                                                                    IsActive = true,
                                                                    Description = "this is p3"
                                                                },
                                                            new Project
                                                                {
                                                                    Name = "P4",
                                                                    CreateDate = DateTime.Now,
                                                                    IsActive = true,
                                                                    Description = "this is p4"
                                                                }
                                                        };

        private readonly IList<Head> _heads = new List<Head>
                                                  {
                                                      new Head
                                                          {
                                                              Name = "Bank Book",
                                                              IsActive = true,
                                                              HeadType = "Capital",
                                                              Description = "Default Bank Book"
                                                          },
                                                      new Head
                                                          {
                                                              Name = "Cash Book",
                                                              IsActive = true,
                                                              HeadType = "Capital",
                                                              Description = "Default Cash Book"
                                                          },
                                                      new Head
                                                          {
                                                              Name = "Adjustment",
                                                              IsActive = true,
                                                              HeadType = "Revenue",
                                                              Description = "This head created for doing adjustment"
                                                          },
                                                      new Head
                                                          {
                                                              Name = "h1",
                                                              IsActive = true,
                                                              HeadType = "Capital",
                                                              Description = "this is h1"
                                                          },
                                                      new Head
                                                          {
                                                              Name = "h2",
                                                              IsActive = true,
                                                              HeadType = "Revenue",
                                                              Description = "this is h2"
                                                          },
                                                      new Head
                                                          {
                                                              Name = "h3",
                                                              IsActive = true,
                                                              HeadType = "Capital",
                                                              Description = "this is h3"
                                                          }
                                                  };

    }
}

