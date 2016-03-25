namespace SurveillanceCam2DB.ConsoleForTestingPurpose
{
    using SurveillanceCam2DB.Model;

    using System.Collections.Generic;
    using System.Data.Entity;

    public class DefaultDataDbInitializer : DropCreateDatabaseAlways<SurveillanceCam2DBContext>
    {

        private List<ActionType> ActionTypes()
        {
            var tellstickActionTypes = new List<ActionType>
            {
                new ActionType { Active = true, Name = Model.Enums.ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB},
                new ActionType { Active = true, Name = Model.Enums.ActionTypes.Dont_CopyImageFrom_SurveillanceCamera2DB}
            };
            return tellstickActionTypes;
        }

        private List<Position> Positions()
        {
            var positions = new List<Position>
            {
                new Position { Active = true, Description = "Liggandes på besöksstolen stolen i Hans rum" },
                new Position { Active = true, Description = "Garage" },
                new Position { Active = true, Description = "Garage - mot biluppställnigsplats" },
                new Position { Active = true, Description = "Garage - mot snickarverkstad" },
                new Position { Active = true, Description = "Garage - mot fönster" },
                new Position { Active = true, Description = "Lekstugan" },
                new Position { Active = true, Description = "Växthuset" },
                new Position { Active = true, Description = "Uppfarten" },
                new Position { Active = true, Description = "Huset - öster" },
                new Position { Active = true, Description = "Huset - norr" },
                new Position { Active = true, Description = "Huset - väster" },
                new Position { Active = true, Description = "Altanen" },
                new Position { Active = true, Description = "Groventrén (dörren)" },
                new Position { Active = true, Description = "Grovkök" },
                new Position { Active = true, Description = "Grovkök - mot fönstret" },
                new Position { Active = true, Description = "Grovkök - mot köket" },
                new Position { Active = true, Description = "Vind" },
                new Position { Active = true, Description = "Vind - mot norr" },
                new Position { Active = true, Description = "Vind - mot söder" },
                new Position { Active = true, Description = "Kök" },
                new Position { Active = true, Description = "Kök - mot groventren" },
                new Position { Active = true, Description = "Kök - mot hallen" },
                new Position { Active = true, Description = "Kök - mot spis" },
                new Position { Active = true, Description = "Kök - mot kylskåp" },
                new Position { Active = true, Description = "Hall" },
                new Position { Active = true, Description = "Hall - mot finingång" },
                new Position { Active = true, Description = "Hall - mot vardagsrum" },
                new Position { Active = true, Description = "Gästtoalett" },
                new Position { Active = true, Description = "Hall - gång mot TV-rum" },
                new Position { Active = true, Description = "Badrum - mot fönster" },
                new Position { Active = true, Description = "Badrum - mot dörr" },
                new Position { Active = true, Description = "TV-rum" },
                new Position { Active = true, Description = "TV-rum - mot Atikas rum" },
                new Position { Active = true, Description = "TV-rum - mot badrum" },
                new Position { Active = true, Description = "TV-rum - mot fönster" },
                new Position { Active = true, Description = "TV-rum - mot Hans rum" },
                new Position { Active = true, Description = "Atikas rum" },
                new Position { Active = true, Description = "Atikas rum - mot fönster" },
                new Position { Active = true, Description = "Atikas rum - mot dörr" },
                new Position { Active = true, Description = "Atikas rum - mot vardagsrum" },
                new Position { Active = true, Description = "Atikas rum - mot sovrum" },
                new Position { Active = true, Description = "Hans rum" },
                new Position { Active = true, Description = "Hans rum - mot sovrum" },
                new Position { Active = true, Description = "Hans rum - mot fönster" },
                new Position { Active = true, Description = "Hans rum - mot TV-rum" },
                new Position { Active = true, Description = "Hans rum - mot norr" },
                new Position { Active = true, Description = "Sovrum" },
                new Position { Active = true, Description = "Sovrum - mot Atikas rum" },
                new Position { Active = true, Description = "Sovrum - mot Hans rum" },
                new Position { Active = true, Description = "Sovrum - mot fönster" },
                new Position { Active = true, Description = "Sovrum - mot norr" },
                new Position { Active = true, Description = "Sovrum" },
                new Position { Active = true, Description = "Sovrum" },
                new Position { Active = true, Description = "Vardagsrum" },
                new Position { Active = true, Description = "Vardagsrum - mot Atikas rum" },
                new Position { Active = true, Description = "Vardagsrum - mot hall" },
                new Position { Active = true, Description = "Vardagsrum - mot uterum" },
                new Position { Active = true, Description = "Vardagsrum - mot fönster" },
                new Position { Active = true, Description = "Uterum" },
                new Position { Active = true, Description = "Uterum - mot altan" },
                new Position { Active = true, Description = "Uterum - mot väster" },
                new Position { Active = true, Description = "Uterum - mot norr" },
                new Position { Active = true, Description = "Uterum - mot vardagsrum" },
                new Position { Active = true, Description = "Tomatkamera" }
            };
            return positions;
        }

        private List<Camera> Cameras()
        {
            var cameras = new List<Camera>
            {
                new Camera { Active = true, GetPicURL = "http://10.0.0.63:10720/photo.jpg", Name = "Samsung Galaxy S4, Skräphögen", CameraNetworkUser = "HaunsTM", CameraNetworkUserPassword = "oV1", DefaultImageQualityPercent  = 50, DefaultMaxImageWidth = 4128, DefaultMaxImageHeight = 3096, PreserveImageAspectRatio = true}
            };
            return cameras;
        }

        private List<Scheduler> Schedulers()
        {
            /* Great help to validate cron expressions:
             * http://crontab.guru/
             */
            var schedulers = new List<Scheduler>
            {
                new Scheduler { Active = true, CronDescription = "Triggar varje sekund", CronExpression = "0/1 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar varannan sekund", CronExpression = "0/2 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 3:e sekund", CronExpression = "0/3 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 4:e sekund", CronExpression = "0/4 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 5:e sekund", CronExpression = "0/5 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 6:e sekund", CronExpression = "0/6 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 7:e sekund", CronExpression = "0/7 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 8:e sekund", CronExpression = "0/8 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 9:e sekund", CronExpression = "0/9 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 10:e sekund", CronExpression = "0/10 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 20:e sekund", CronExpression = "0/30 * * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 30:e sekund", CronExpression = "0/30 * * * * ?"},

                new Scheduler { Active = true, CronDescription = "Triggar varje minut", CronExpression = "* * * * *"},
                new Scheduler { Active = true, CronDescription = "Triggar varannan minut", CronExpression = "0 0/2 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 3:e minut", CronExpression = "0 0/3 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 4:e minut", CronExpression = "0 0/4 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 5:e minut", CronExpression = "0 0/5 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 6:e minut", CronExpression = "0 0/6 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 7:e minut", CronExpression = "0 0/7 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 8:e minut", CronExpression = "0 0/8 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 9:e minut", CronExpression = "0 0/9 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 10:e minut", CronExpression = "0 0/10 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 15:e minut", CronExpression = "0 0/15 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 20:e minut", CronExpression = "0 0/20 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 25:e minut", CronExpression = "0 0/25 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 30:e minut", CronExpression = "0 0/30 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 35:e minut", CronExpression = "0 0/35 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 40:e minut", CronExpression = "0 0/40 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 45:e minut", CronExpression = "0 0/45 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 50:e minut", CronExpression = "0 0/50 * * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 55:e minut", CronExpression = "0 0/55 * * * ?"},

                new Scheduler { Active = true, CronDescription = "Triggar varje sekund, kl 06 - 22", CronExpression = "*/1 * 6-22 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar varje minut, kl 06 - 22", CronExpression = "*/1 6-22 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar var 5:e minut, Måndag - Fredag", CronExpression = "*/5 * * * MON-FRI"},

                new Scheduler { Active = true, CronDescription = "Triggar kl 00:00 varje dag", CronExpression = "0 0 0 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 00:15 varje dag", CronExpression = "0 15 0 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 00:30 varje dag", CronExpression = "0 30 0 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 00:45 varje dag", CronExpression = "0 45 0 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 01:00 varje dag", CronExpression = "0 0 1 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 01:15 varje dag", CronExpression = "0 15 1 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 01:30 varje dag", CronExpression = "0 30 1 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 01:45 varje dag", CronExpression = "0 45 1 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 02:00 varje dag", CronExpression = "0 0 2 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 02:15 varje dag", CronExpression = "0 15 2 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 02:30 varje dag", CronExpression = "0 30 2 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 02:45 varje dag", CronExpression = "0 45 2 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 03:00 varje dag", CronExpression = "0 0 3 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 03:15 varje dag", CronExpression = "0 15 3 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 03:30 varje dag", CronExpression = "0 30 3 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 03:45 varje dag", CronExpression = "0 45 3 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 04:00 varje dag", CronExpression = "0 0 4 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 04:15 varje dag", CronExpression = "0 15 4 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 04:30 varje dag", CronExpression = "0 30 4 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 04:45 varje dag", CronExpression = "0 45 4 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 05:00 varje dag", CronExpression = "0 0 5 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 05:15 varje dag", CronExpression = "0 15 5 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 05:30 varje dag", CronExpression = "0 30 5 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 05:45 varje dag", CronExpression = "0 45 5 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 06:00 varje dag", CronExpression = "0 0 6 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 06:15 varje dag", CronExpression = "0 15 6 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 06:30 varje dag", CronExpression = "0 30 6 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 06:45 varje dag", CronExpression = "0 45 6 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 07:00 varje dag", CronExpression = "0 0 7 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 07:15 varje dag", CronExpression = "0 15 7 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 07:30 varje dag", CronExpression = "0 30 7 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 07:45 varje dag", CronExpression = "0 45 7 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 08:00 varje dag", CronExpression = "0 0 8 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 08:15 varje dag", CronExpression = "0 15 8 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 08:30 varje dag", CronExpression = "0 30 8 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 08:45 varje dag", CronExpression = "0 45 8 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 09:00 varje dag", CronExpression = "0 0 9 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 09:15 varje dag", CronExpression = "0 15 9 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 09:30 varje dag", CronExpression = "0 30 9 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 09:45 varje dag", CronExpression = "0 45 9 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 10:00 varje dag", CronExpression = "0 0 10 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 10:15 varje dag", CronExpression = "0 15 10 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 10:30 varje dag", CronExpression = "0 30 10 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 10:45 varje dag", CronExpression = "0 45 10 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 11:00 varje dag", CronExpression = "0 0 11 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 11:15 varje dag", CronExpression = "0 15 11 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 11:30 varje dag", CronExpression = "0 30 11 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 11:45 varje dag", CronExpression = "0 45 11 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 12:00 varje dag", CronExpression = "0 0 12 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 12:15 varje dag", CronExpression = "0 15 12 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 12:30 varje dag", CronExpression = "0 30 12 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 12:45 varje dag", CronExpression = "0 45 12 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 13:00 varje dag", CronExpression = "0 0 13 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 13:15 varje dag", CronExpression = "0 15 13 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 13:30 varje dag", CronExpression = "0 30 13 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 13:45 varje dag", CronExpression = "0 45 13 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 14:00 varje dag", CronExpression = "0 0 14 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 14:15 varje dag", CronExpression = "0 15 14 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 14:30 varje dag", CronExpression = "0 30 14 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 14:45 varje dag", CronExpression = "0 45 14 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 15:00 varje dag", CronExpression = "0 0 15 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 15:15 varje dag", CronExpression = "0 15 15 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 15:30 varje dag", CronExpression = "0 30 15 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 15:45 varje dag", CronExpression = "0 45 15 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 16:00 varje dag", CronExpression = "0 0 16 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 16:15 varje dag", CronExpression = "0 15 16 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 16:30 varje dag", CronExpression = "0 30 16 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 16:45 varje dag", CronExpression = "0 45 16 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 17:00 varje dag", CronExpression = "0 0 17 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 17:15 varje dag", CronExpression = "0 15 17 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 17:30 varje dag", CronExpression = "0 30 17 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 17:45 varje dag", CronExpression = "0 45 17 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 18:00 varje dag", CronExpression = "0 0 18 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 18:15 varje dag", CronExpression = "0 15 18 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 18:30 varje dag", CronExpression = "0 30 18 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 18:45 varje dag", CronExpression = "0 45 18 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 19:00 varje dag", CronExpression = "0 0 19 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 19:15 varje dag", CronExpression = "0 15 19 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 19:30 varje dag", CronExpression = "0 30 19 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 19:45 varje dag", CronExpression = "0 45 19 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 20:00 varje dag", CronExpression = "0 0 20 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 20:15 varje dag", CronExpression = "0 15 20 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 20:30 varje dag", CronExpression = "0 30 20 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 20:45 varje dag", CronExpression = "0 45 20 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 21:00 varje dag", CronExpression = "0 0 21 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 21:15 varje dag", CronExpression = "0 15 21 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 21:30 varje dag", CronExpression = "0 30 21 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 21:45 varje dag", CronExpression = "0 45 21 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 22:00 varje dag", CronExpression = "0 0 22 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 22:15 varje dag", CronExpression = "0 15 22 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 22:30 varje dag", CronExpression = "0 30 22 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 22:45 varje dag", CronExpression = "0 45 22 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 23:00 varje dag", CronExpression = "0 0 23 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 23:15 varje dag", CronExpression = "0 15 23 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 23:30 varje dag", CronExpression = "0 30 23 * * ?"},
                new Scheduler { Active = true, CronDescription = "Triggar kl 23:45 varje dag", CronExpression = "0 45 23 * * ?"}
            };

            return schedulers;
        }

        protected override void Seed(SurveillanceCam2DBContext context)
        {
            context.ActionTypes.AddRange(this.ActionTypes());
            context.Schedulers.AddRange(this.Schedulers());
            context.Positions.AddRange(this.Positions());
            context.Cameras.AddRange(this.Cameras());

            context.SaveChanges();
        }
    }
}
