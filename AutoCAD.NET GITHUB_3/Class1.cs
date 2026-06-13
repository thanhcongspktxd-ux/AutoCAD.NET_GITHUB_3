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
        [CommandMethod("DRAWLINE")]
        public void DrawLine()
        {
            // Lấy Document hiện tại
            Document doc = Application.DocumentManager.MdiActiveDocument;

            // Lấy Database của bản vẽ
            Database db = doc.Database;

            // Tạo 2 điểm đầu cuối
            Point3d pt1 = new Point3d(0, 0, 0);
            Point3d pt2 = new Point3d(100, 50, 0);

            // Bắt đầu Transaction
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // Mở BlockTable
                BlockTable bt =
                    (BlockTable)tr.GetObject(
                        db.BlockTableId,
                        OpenMode.ForRead);

                // Mở ModelSpace để ghi dữ liệu
                BlockTableRecord btr =
                    (BlockTableRecord)tr.GetObject(
                        bt[BlockTableRecord.ModelSpace],
                        OpenMode.ForWrite);

                // Tạo đối tượng Line
                Line line = new Line(pt1, pt2);

                // Thêm Line vào ModelSpace
                btr.AppendEntity(line);

                // Đăng ký Line với Transaction
                tr.AddNewlyCreatedDBObject(line, true);

                // Lưu thay đổi
                tr.Commit();
            }
        }


    }
}
