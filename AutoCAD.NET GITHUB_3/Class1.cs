using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Runtime.ConstrainedExecution;
namespace AutoCAD.NET_GITHUB_3
{
    public class Class1
    {
        [CommandMethod("USERCIRCLE")]
        public void UserCircle()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            // Chọn tâm

            PromptPointOptions ppo = new PromptPointOptions("\nChọn tâm đường tròn: ");

            PromptPointResult ppr = ed.GetPoint(ppo);

            if (ppr.Status != PromptStatus.OK)
            {
                return;
            }
               

            Point3d center = ppr.Value;

            // Nhập bán kính

            PromptDoubleOptions pdo = new PromptDoubleOptions("\nNhập bán kính:");

            pdo.AllowNegative = false;

            pdo.AllowZero = false;

            PromptDoubleResult pdr = ed.GetDouble(pdo);

            if (pdr.Status != PromptStatus.OK)
            {
                return;
            }

            double radius = pdr.Value;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId,OpenMode.ForRead);

                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace],OpenMode.ForWrite);

                Circle circle = new Circle(center,Vector3d.ZAxis , radius);

                btr.AppendEntity(circle);

                tr.AddNewlyCreatedDBObject(circle, true);

                tr.Commit();
            }
        }
    }
}
