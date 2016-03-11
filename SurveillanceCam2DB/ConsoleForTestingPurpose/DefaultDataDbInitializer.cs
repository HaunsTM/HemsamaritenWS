namespace SurveillanceCam2DB.ConsoleForTestingPurpose
{
    using System.Collections.Generic;

    using Model;
    using System.Data.Entity;
    using System.Drawing;
    using System.Linq;

    using SurveillanceCam2DB.Model.Enums;

    public class DefaultDataDbInitializer : DropCreateDatabaseIfModelChanges<SurveillanceCam2DBContext>
    {
        protected override void Seed(SurveillanceCam2DBContext context)
        {

            context.Cameras.Add(new Camera { Active = true, GetPicURL = "http://10.0.0.63:10720/photo.jpg", Name = "Samsung Galaxy S4, Skräphögen", CameraNetworkUser = "HaunsTM", CameraNetworkUserPassword = "oV1", DefaultImageQualityPercent  = 50, DefaultMaxImageWidth = 500, DefaultMaxImageHeight = 500, PreserveImageAspectRatio = true});

            context.Positions.AddRange( new List<Position>
            {
                new Position { Active = true, Description = "Liggandes på besöksstolen stolen i Hans rum" },
                new Position { Active = true, Description = "Tomatkamera" }
            });

            context.ActionTypes.AddRange(new List<ActionType>
                                             {
                                                 new ActionType { Active = true, Type = ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB},
                                                 new ActionType { Active = true, Type = ActionTypes.Dont_CopyImageFrom_SurveillanceCamera2DB}
                                             });

            context.SaveChanges();

            context.Actions.Add(
                new Action
                    {
                        Active = true,
                        ActionType = (from actType in context.ActionTypes
                                      where actType.Type == ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB
                                      select actType).First(),
                        Camera =
                            (from cam in context.Cameras
                             where cam.Name == "Samsung Galaxy S4, Skräphögen"
                             select cam).First(),
                        CronExpression = "0/10 * * * * ?"
                });

            context.SaveChanges();
        }
    }
}
