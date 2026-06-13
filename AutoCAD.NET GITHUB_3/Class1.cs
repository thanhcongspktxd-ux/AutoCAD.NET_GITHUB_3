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
        [CommandMethod("USERLINE")]
        public void UserLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;


            Database db = doc.Database;

            Editor ed = doc.Editor;

            // Chọn điểm đầu

            PromptPointOptions ppo1 = new PromptPointOptions("\nChọn điểm đầu tiên: ");

            PromptPointResult ppr1 = ed.GetPoint(ppo1);

    if (ppr1.Status != PromptStatus.OK)
            {
                return;
            }


            Point3d pt1 = ppr1.Value;

            // Chọn điểm cuối

            PromptPointOptions ppo2 = new PromptPointOptions("\nChọn điểm thứ hai: ");

            ppo2.BasePoint = pt1;

            ppo2.UseBasePoint = true;

            PromptPointResult ppr2 = ed.GetPoint(ppo2);

            if (ppr1.Status != PromptStatus.OK)
            {
                return;
            }

            Point3d pt2 = ppr2.Value;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId,OpenMode.ForRead);

                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace],OpenMode.ForWrite);

                Line line = new Line(pt1,pt2);

                btr.AppendEntity(line);

                tr.AddNewlyCreatedDBObject(line, true);

                tr.Commit();
            }
        }
    }
 }

