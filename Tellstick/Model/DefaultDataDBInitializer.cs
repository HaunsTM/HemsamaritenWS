namespace Tellstick.Model
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Tellstick.Model.Enums;

    public class DefaultDataDbInitializer : DropCreateDatabaseIfModelChanges<TellstickDBContext>
    {
        protected override void Seed(TellstickDBContext context)
        {
            context.TellstickActionTypes.AddRange(new List<TellstickActionType>
                                                      {
                                                          new TellstickActionType { Active = true, Type = Enums.EnumTellstickActionType.TurnOn},
                                                          new TellstickActionType { Active = true, Type = Enums.EnumTellstickActionType.TurnOff},
                                                          new TellstickActionType { Active = true, Type = Enums.EnumTellstickActionType.TurnOff}
                                                      });

            context.TellstickProtocols.Add(
                new TellstickProtocol { Active = true, Type = Enums.EnumTellstickProtocol.arctech }
            );

            context.TellstickModels.Add(
                new TellstickModel { Active = true, Type = Enums.EnumTellstickModelType.codeswitch, Manufacturer = EnumTellstickModelManufacturer.Nexa }
                );
            context.TellstickSchedulers.AddRange( new List<TellstickScheduler>
            {
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 10:e sekund", CronExpression = "0/3 * * * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 10:e sekund", CronExpression = "0/8 * * * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 10:e sekund", CronExpression = "0/10 * * * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 30:e sekund", CronExpression = "0/30 * * * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar varje minut", CronExpression = "* * * * *"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 5:e minut", CronExpression = "0 0/5 * * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 5:e minut, kl 15, Måndag - Fredag", CronExpression = "*/5 15 * * MON-FRI"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 10:e minut", CronExpression = "0 0/10 * * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar var 30:e minut", CronExpression = "0 0/30 * * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 00 varje dag", CronExpression = "0 0 0 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 01 varje dag", CronExpression = "0 0 1 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 02 varje dag", CronExpression = "0 0 2 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 03 varje dag", CronExpression = "0 0 3 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 04 varje dag", CronExpression = "0 0 4 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 05 varje dag", CronExpression = "0 0 5 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 06 varje dag", CronExpression = "0 0 6 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 07 varje dag", CronExpression = "0 0 7 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 08 varje dag", CronExpression = "0 0 8 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 09 varje dag", CronExpression = "0 0 9 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 12 varje dag", CronExpression = "0 0 10 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 11 varje dag", CronExpression = "0 0 11 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 12 varje dag", CronExpression = "0 0 12 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 13 varje dag", CronExpression = "0 0 13 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 14 varje dag", CronExpression = "0 0 14 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 15 varje dag", CronExpression = "0 0 15 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 16 varje dag", CronExpression = "0 0 16 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 17 varje dag", CronExpression = "0 0 17 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 18 varje dag", CronExpression = "0 0 18 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 19 varje dag", CronExpression = "0 0 19 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 20 varje dag", CronExpression = "0 0 20 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 21 varje dag", CronExpression = "0 0 21 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 22 varje dag", CronExpression = "0 0 22 * * ?"},
                new TellstickScheduler { Active = true, CronDescription = "Triggar kl 23 varje dag", CronExpression = "0 0 23 * * ?"}
            });

            context.TellstickParameters.AddRange( new List<TellstickParameter>
            {
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._1, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._2, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._3, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._4, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._5, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._6, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._7, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._8, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._9, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._10, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._11, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._12, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._13, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._14, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._15, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._16, House = EnumTellstickParameter_House.A },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._1, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._2, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._3, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._4, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._5, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._6, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._7, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._8, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._9, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._10, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._11, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._12, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._13, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._14, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._15, House = EnumTellstickParameter_House.B },
                new TellstickParameter { Active = true, Unit = EnumTellstickParameter_Unit._16, House = EnumTellstickParameter_House.B },
            });


            context.SaveChanges();

            #region Test data

            var artectProtocol =
                (from prot in context.TellstickProtocols where prot.Type == EnumTellstickProtocol.arctech select prot)
                    .First();

            var nexaModel =
                (from mod in context.TellstickModels where mod.Type == EnumTellstickModelType.codeswitch && mod.Manufacturer == EnumTellstickModelManufacturer.Nexa select mod)
                    .First();

            context.TellstickUnits.Add(
                new TellstickUnit
                {
                    Active = true,
                    Name="Unit 9",
                    LocationDesciption = "Hans skrivbord",
                    TellstickProtocol = artectProtocol,
                    TellstickModel = nexaModel,
                    TellstickParameter = (from par in context.TellstickParameters where par.Unit == EnumTellstickParameter_Unit._9 && par.House == EnumTellstickParameter_House.A select par).First()
                });

            context.SaveChanges();

            var tellstickUnit9 =
                (from unit in context.TellstickUnits where unit.Name == "Unit 9" select unit)
                    .First();

            var schedulerOnTrigger =
                (from trig in context.TellstickSchedulers where trig.CronExpression == "0/8 * * * * ?" select trig)
                    .First();
            var schedulerOffTrigger =
                (from trig in context.TellstickSchedulers where trig.CronExpression == "0/3 * * * * ?" select trig)
                    .First();

            var onAction =
                (from act in context.TellstickActionTypes where act.Type == EnumTellstickActionType.TurnOn select act)
                    .First();

            var offAction =
                (from act in context.TellstickActionTypes where act.Type == EnumTellstickActionType.TurnOff select act)
                    .First();

            context.TellstickActions.AddRange( new List<TellstickAction>
                {
                    new TellstickAction { Active = true, TellstickUnit = tellstickUnit9, TellstickActionType = onAction, TellstickScheduler = schedulerOnTrigger},
                    new TellstickAction { Active = true, TellstickUnit = tellstickUnit9, TellstickActionType = offAction, TellstickScheduler = schedulerOffTrigger}
                });

            context.SaveChanges();

            #endregion
            /*
            

            context.ActionTypes.AddRange(new List<ActionType>
                                             {
                                                 new ActionType { Active = true, Type = ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB},
                                                 new ActionType { Active = true, Type = ActionTypes.Dont_CopyImageFrom_SurveillanceCamera2DB}
                                             });

            context.SaveChanges();

            
            
            */

        }
    }
}