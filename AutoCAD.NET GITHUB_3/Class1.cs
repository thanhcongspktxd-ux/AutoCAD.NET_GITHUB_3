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
        [CommandMethod("POLYLINE1")]
        public void Polyline1()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
               BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                Polyline pl = new Polyline();

                pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);


                pl.AddVertexAt(1, new Point2d(100, 0), 0, 0, 0);


                pl.AddVertexAt(2, new Point2d(100, 50), 0, 0, 0);


                pl.AddVertexAt(3, new Point2d(200, 50), 0, 0, 0);


                btr.AppendEntity(pl);

                tr.AddNewlyCreatedDBObject(pl, true);

                tr.Commit();
            }
        }
    }
}
