using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeoFun.Spatial
{
    public class SHPFile
    {
        /// <summary>
        /// 输入路径
        /// </summary>
        public string PathSrc { get; set; } = "";

        /// <summary>
        /// 输出路径
        /// </summary>
        public string PathDst { get; set; } = "";

        /// <summary>
        /// 文件头
        /// </summary>
        public SHPFileHeader Header = new SHPFileHeader();

        /// <summary>
        /// 要素类型
        /// </summary>
        public enumShapeType ShapeType
        {
            get
            {
                return Header.ShapeType;
            }
        }

        /// <summary>
        /// 文件流
        /// </summary>
        public BinaryReader Reader = null;

        /// <summary>
        /// 写文件流
        /// </summary>
        public BinaryWriter Writer = null;

        public static List<string> FileAppends = new List<string>() { "shp", "shp.xml", "cpg", "dbf", "prj", "sbn", "sbx", "shx" };

        public SHPFile(string pathSrc = "", string pathDst = "")
        {
            PathSrc = pathSrc;
            PathDst = pathDst;
        }

        public bool OpenRead()
        {
            if (!File.Exists(PathSrc)) return false;

            Reader = new BinaryReader(File.OpenRead(PathSrc));

            return true;
        }
        public void CloseRead()
        {
            try
            {
                if (Reader != null)
                {
                    Reader.Close();
                }
            }
            catch
            { }
        }

        public bool OpenWrite()
        {
            Writer = new BinaryWriter(File.OpenWrite(PathDst));
            return true;
        }
        public void CloseWrite()
        {
            try
            {
                if (Writer != null)
                {
                    Writer.Close();
                }

                CopyOtherFiles(PathSrc, PathDst);
            }
            catch
            { }
        }

        public static void CopyOtherFiles(string pathSrc, string pathDst)
        {
            string path1, path2;
            for (int i = 0; i < FileAppends.Count; i++)
            {
                if (FileAppends[i] == "shp") continue;
                try
                {
                    path1 = pathSrc.Substring(0, pathSrc.Length - 4) + "." + FileAppends[i];
                    path2 = pathDst.Substring(0, pathDst.Length - 4) + "." + FileAppends[i];

                    if (File.Exists(path1))
                    {
                        FileInfo info = new FileInfo(path1);

                        info.CopyTo(path2, true);
                    }

                    //path1 = pathSrc.Substring(0, pathSrc.Length - 4) + "." + OtherFiles[i].ToUpper();
                    //path2 = pathDst.Substring(0, pathDst.Length - 4) + "." + OtherFiles[i].ToUpper();

                    //if(File.Exists(path1))
                    //{
                    //    FileInfo info = new FileInfo(path1);

                    //    info.CopyTo(path2);
                    //}
                }
                catch
                {
                    continue;
                }
            }
        }

        public static void Copy(string pathSrc, string pathDst)
        {
            string path1, path2;
            for (int i = 0; i < FileAppends.Count; i++)
            {
                try
                {
                    path1 = pathSrc.Substring(0, pathSrc.Length - 4) + "." + FileAppends[i];
                    path2 = pathDst.Substring(0, pathDst.Length - 4) + "." + FileAppends[i];

                    if (File.Exists(path1))
                    {
                        FileInfo info = new FileInfo(path1);

                        info.CopyTo(path2);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        public static void Rename(string pathSrc, string pathDst)
        {
            if (pathSrc == pathDst) return;

            string path1, path2;
            for (int i = 0; i < FileAppends.Count; i++)
            {
                try
                {
                    path1 = pathSrc.Substring(0, pathSrc.Length - 4) + "." + FileAppends[i];
                    path2 = pathDst.Substring(0, pathDst.Length - 4) + "." + FileAppends[i];

                    if (File.Exists(path1))
                    {
                        FileInfo info = new FileInfo(path1);

                        info.MoveTo(path2);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// 读文件头
        /// </summary>
        /// <returns></returns>
        public bool ReadHeader()
        {
            if (!OpenRead()) return false;

            byte[] buffer = Reader.ReadBytes(100);
            DecodeHeader(ref buffer);

            return true;
        }

        /// <summary>
        /// 写文件头
        /// </summary>
        /// <returns></returns>
        public bool WriteHeader()
        {
            if (Writer is null && !OpenWrite()) return false;

            Writer.Write(BitConverter.GetBytes(Header.FileCode).Reverse().ToArray());
            Writer.Write(BitConverter.GetBytes(Header.Unuse1).Reverse().ToArray());
            Writer.Write(BitConverter.GetBytes(Header.Unuse2).Reverse().ToArray());
            Writer.Write(BitConverter.GetBytes(Header.Unuse3).Reverse().ToArray());
            Writer.Write(BitConverter.GetBytes(Header.Unuse4).Reverse().ToArray());
            Writer.Write(BitConverter.GetBytes(Header.Unuse5).Reverse().ToArray());
            Writer.Write(BitConverter.GetBytes(Header.FileLength).Reverse().ToArray());

            Writer.Write(BitConverter.GetBytes(Header.Version));
            Writer.Write(BitConverter.GetBytes((int)(Header.ShapeType)));
            Writer.Write(BitConverter.GetBytes(Header.Xmin));
            Writer.Write(BitConverter.GetBytes(Header.Ymin));
            Writer.Write(BitConverter.GetBytes(Header.Xmax));
            Writer.Write(BitConverter.GetBytes(Header.Ymax));
            Writer.Write(BitConverter.GetBytes(Header.Zmin));
            Writer.Write(BitConverter.GetBytes(Header.Zmax));
            Writer.Write(BitConverter.GetBytes(Header.Mmin));
            Writer.Write(BitConverter.GetBytes(Header.Mmax));

            return true;
        }

        /// <summary>
        /// 解码文件头
        /// </summary>
        /// <param name="buffer"></param>
        private void DecodeHeader(ref byte[] buffer)
        {
            Header.FileCode = BitConverter.ToInt32(buffer.Take(4).Reverse().ToArray(), 0);
            Header.Unuse1 = BitConverter.ToInt32(buffer.Skip(4).Take(4).Reverse().ToArray(), 0);
            Header.Unuse2 = BitConverter.ToInt32(buffer.Skip(8).Take(4).Reverse().ToArray(), 0);
            Header.Unuse3 = BitConverter.ToInt32(buffer.Skip(12).Take(4).Reverse().ToArray(), 0);
            Header.Unuse4 = BitConverter.ToInt32(buffer.Skip(16).Take(4).Reverse().ToArray(), 0);
            Header.Unuse5 = BitConverter.ToInt32(buffer.Skip(20).Take(4).Reverse().ToArray(), 0);
            Header.FileLength = BitConverter.ToInt32(buffer.Skip(24).Take(4).Reverse().ToArray(), 0);

            Header.Version = BitConverter.ToInt32(buffer.Skip(28).Take(4).ToArray(), 0);
            Header.ShapeType = (enumShapeType)BitConverter.ToInt32(buffer.Skip(32).Take(4).ToArray(), 0);             //shape类型
            Header.Xmin = BitConverter.ToDouble(buffer.Skip(36).Take(8).ToArray(), 0);
            Header.Ymin = BitConverter.ToDouble(buffer.Skip(44).Take(8).ToArray(), 0);
            Header.Xmax = BitConverter.ToDouble(buffer.Skip(52).Take(8).ToArray(), 0);
            Header.Ymax = BitConverter.ToDouble(buffer.Skip(60).Take(8).ToArray(), 0);
            Header.Zmin = BitConverter.ToDouble(buffer.Skip(68).Take(8).ToArray(), 0);
            Header.Zmax = BitConverter.ToDouble(buffer.Skip(76).Take(8).ToArray(), 0);
            Header.Mmin = BitConverter.ToDouble(buffer.Skip(84).Take(8).ToArray(), 0);
            Header.Mmax = BitConverter.ToDouble(buffer.Skip(92).Take(8).ToArray(), 0);
        }

        private IFeature DecodeFeature(ref byte[] buffer)
        {
            IFeature feature = null;

            switch (ShapeType)
            {
                case enumShapeType.NullShape:
                    break;

                case enumShapeType.Point:
                    feature = DecodePoint(ref buffer);
                    break;
                case enumShapeType.PointM:
                    feature = DecodePointM(ref buffer);
                    break;
                case enumShapeType.PointZ:
                    feature = DecodePointZ(ref buffer);
                    break;

                case enumShapeType.Polyline:
                    feature = DecodePolyLine(ref buffer);
                    break;
                case enumShapeType.PolylineM:
                    feature = DecodePolyLineM(ref buffer);
                    break;
                case enumShapeType.PolylineZ:
                    feature = DecodePolyLineZ(ref buffer);
                    break;

                case enumShapeType.Polygon:
                    feature = DecodePolygon(ref buffer);
                    break;
                case enumShapeType.PolygonM:
                    feature = DecodePolygonM(ref buffer);
                    break;
                case enumShapeType.PolygonZ:
                    feature = DecodePolygonZ(ref buffer);
                    break;

                case enumShapeType.MultiPoint:
                    feature = DecodeMultiPoint(ref buffer);
                    break;
                case enumShapeType.MultiPointM:
                    feature = DecodeMultiPointM(ref buffer);
                    break;
                case enumShapeType.MultiPointZ:
                    feature = DecodeMultiPointZ(ref buffer);
                    break;

                case enumShapeType.MultiPatch:
                    feature = DecodeMultipatch(ref buffer);
                    break;
            }

            return feature;
        }
        private bool WriteFeature(IFeature ft)
        {
            switch (ShapeType)
            {
                case enumShapeType.NullShape:
                    return WriteNull(ft as ShapeNull);

                case enumShapeType.Point:
                    return WritePoint(ft as Point);
                case enumShapeType.PointM:
                    return WritePointM(ft as Point);
                case enumShapeType.PointZ:
                    return WritePointZ(ft as Point);

                case enumShapeType.Polyline:
                    return WritePolyLine(ft as PolyLine);
                case enumShapeType.PolylineM:
                    return WritePolyLineM(ft as PolyLine);
                case enumShapeType.PolylineZ:
                    return WritePolyLineZ(ft as PolyLine);

                case enumShapeType.Polygon:
                    return WritePolygon(ft as Polygon);
                case enumShapeType.PolygonM:
                    return WritePolygonM(ft as Polygon);
                case enumShapeType.PolygonZ:
                    return WritePolygonZ(ft as Polygon);

                case enumShapeType.MultiPoint:
                    return WriteMultiPoint(ft as MultiPoint);
                case enumShapeType.MultiPointM:
                    return WriteMultiPointM(ft as MultiPoint);
                case enumShapeType.MultiPointZ:
                    return WriteMultiPointZ(ft as MultiPoint);

                case enumShapeType.MultiPatch:
                    return WriteMultiPatch(ft as MultiPatch);

                default:
                    return false;
            }
        }

        /// <summary>
        /// 读取要素记录
        /// </summary>
        /// <param name="readNum">需要读取的个数</param>
        /// <param name="progressCallBack">读取进度调用的方法</param>
        /// <returns></returns>
        public List<SHPRecord> ReadRecords(uint readNum = 1000, Action progressCallBack = null)
        {
            List<SHPRecord> records = new List<SHPRecord>();

            uint i = 0;
            SHPRecord record;
            for (; i < readNum; i++)
            {
                if (!ReadRecord(out record)) break;

                records.Add(record);
            }

            readNum = i + 1;
            return records;
        }
        public int WriteRecords(List<SHPRecord> records)
        {
            if (records is null) return 0;

            int writeNum = 0;
            for (int i = 0; i < records.Count; i++)
            {
                if (WriteRecord(records[i]))
                {
                    writeNum++;
                }
            }

            return writeNum;
        }

        public bool ReadRecord(out SHPRecord record)
        {
            record = new SHPRecord();

            try
            {
                record.RecordNum = BitConverter.ToInt32(Reader.ReadBytes(4).Reverse().ToArray(), 0);
                record.ContentLen = BitConverter.ToInt32(Reader.ReadBytes(4).Reverse().ToArray(), 0);

                byte[] buffer = Reader.ReadBytes(record.ContentLen * 2);

                IFeature ft = DecodeFeature(ref buffer);
                record.Feature = ft;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public bool WriteRecord(in SHPRecord record)
        {
            Writer.Write(BitConverter.GetBytes(record.RecordNum).Reverse().ToArray());
            Writer.Write(BitConverter.GetBytes(record.ContentLen).Reverse().ToArray());

            WriteFeature(record.Feature);

            return true;
        }

        public bool ReadNull(ref byte[] buffer)
        {
            ShapeNull shpN = new ShapeNull();
            shpN.Data = BitConverter.ToInt32(buffer, 0);
            return true;
        }
        public bool WriteNull(ShapeNull shpNull)
        {
            Writer.Write(enumShapeCode.NullShape);
            Writer.Write(BitConverter.GetBytes(shpNull.Data));
            return true;
        }

        public Point DecodePoint(ref byte[] buffer)
        {
            Point pt = new Point();
            pt.X = BitConverter.ToDouble(buffer, 4);
            pt.Y = BitConverter.ToDouble(buffer, 12);

            return pt;
        }
        public Point DecodePointM(ref byte[] buffer)
        {
            Point pt = new Point();

            pt.HasM = true;
            pt.X = BitConverter.ToDouble(buffer, 4);
            pt.Y = BitConverter.ToDouble(buffer, 12);
            pt.M = BitConverter.ToDouble(buffer, 20);

            return pt;
        }
        public Point DecodePointZ(ref byte[] buffer)
        {
            Point pt = new Point();
            pt.HasM = true;
            pt.X = BitConverter.ToDouble(buffer, 4);
            pt.Y = BitConverter.ToDouble(buffer, 12);
            pt.Z = BitConverter.ToDouble(buffer, 20);
            pt.M = BitConverter.ToDouble(buffer, 28);

            return pt;
        }

        public bool WritePoint(Point pt)
        {
            Writer.Write(enumShapeCode.Point);
            Writer.Write(BitConverter.GetBytes(pt.X));
            Writer.Write(BitConverter.GetBytes(pt.Y));

            return true;
        }
        public bool WritePointM(Point pt)
        {
            Writer.Write(enumShapeCode.PointM);
            Writer.Write(BitConverter.GetBytes(pt.X));
            Writer.Write(BitConverter.GetBytes(pt.Y));
            Writer.Write(BitConverter.GetBytes(pt.M));

            return true;
        }
        public bool WritePointZ(Point pt)
        {
            Writer.Write(enumShapeCode.PointZ);
            Writer.Write(BitConverter.GetBytes(pt.X));
            Writer.Write(BitConverter.GetBytes(pt.Y));
            Writer.Write(BitConverter.GetBytes(pt.M));
            Writer.Write(BitConverter.GetBytes(pt.Z));

            return true;
        }

        public MultiPoint DecodeMultiPoint(ref byte[] buffer)
        {
            MultiPoint mPoint = new MultiPoint();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);
            mPoint.BoundingBox = box;

            mPoint.NumPoints = BitConverter.ToInt32(buffer, 36);
            mPoint.Points = new Point[mPoint.NumPoints];
            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Point pt = new Point();
                pt.X = BitConverter.ToDouble(buffer, 40 + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 40 + 16 * i + 8);
                mPoint.Points[i] = pt;
            }

            return mPoint;
        }
        public MultiPoint DecodeMultiPointM(ref byte[] buffer)
        {
            MultiPoint mPoint = new MultiPoint();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);
            mPoint.BoundingBox = box;

            mPoint.NumPoints = BitConverter.ToInt32(buffer, 36);
            mPoint.Points = new Point[mPoint.NumPoints];
            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Point pt = new Point();
                pt.X = BitConverter.ToDouble(buffer, 40 + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 40 + 16 * i + 8);
                mPoint.Points[i] = pt;
            }

            try
            {
                int X = 40 + 16 * mPoint.NumPoints;
                if (buffer.Length > X)
                {
                    mPoint.HasM = true;
                    mPoint.MRange[0] = BitConverter.ToDouble(buffer, X);
                    mPoint.MRange[1] = BitConverter.ToDouble(buffer, X + 8);
                    for (int i = 0; i < mPoint.NumPoints; i++)
                    {
                        mPoint.Points[i].M = BitConverter.ToDouble(buffer, X + 16 + 8 * i);
                    }
                }
            }
            catch
            { }

            return mPoint;
        }
        public MultiPoint DecodeMultiPointZ(ref byte[] buffer)
        {
            MultiPoint mPoint = new MultiPoint();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);
            mPoint.BoundingBox = box;

            mPoint.NumPoints = BitConverter.ToInt32(buffer, 36);
            mPoint.Points = new Point[mPoint.NumPoints];
            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Point pt = new Point();
                pt.X = BitConverter.ToDouble(buffer, 40 + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 40 + 16 * i + 8);
                mPoint.Points[i] = pt;
            }

            int X = 40 + 16 * mPoint.NumPoints;
            mPoint.ZRange[0] = BitConverter.ToDouble(buffer, X);
            mPoint.ZRange[1] = BitConverter.ToDouble(buffer, X + 8);
            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                mPoint.Points[i].Z = BitConverter.ToDouble(buffer, X + 16 + 8 * i);
            }

            try
            {
                int Y = X + 16 + 8 * mPoint.NumPoints;
                if (buffer.Length > Y)
                {
                    mPoint.HasM = true;

                    mPoint.MRange[0] = BitConverter.ToDouble(buffer, Y);
                    mPoint.MRange[1] = BitConverter.ToDouble(buffer, Y + 8);
                    for (int i = 0; i < mPoint.NumPoints; i++)
                    {
                        mPoint.Points[i].M = BitConverter.ToDouble(buffer, Y + 16 + 8 * i);
                    }
                }
            }
            catch
            { }

            return mPoint;
        }

        public bool WriteMultiPoint(MultiPoint mPoint)
        {
            Writer.Write(enumShapeCode.MultiPoint);
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Ymax));
            Writer.Write(BitConverter.GetBytes(mPoint.NumPoints));

            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].X)); ;
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].Y)); ;
            }

            return true;
        }
        public bool WriteMultiPointM(MultiPoint mPoint)
        {
            Writer.Write(enumShapeCode.MultiPointM);
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Ymax));
            Writer.Write(BitConverter.GetBytes(mPoint.NumPoints));

            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].X)); ;
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].Y)); ;
            }

            if (!mPoint.HasM) return true;

            Writer.Write(BitConverter.GetBytes(mPoint.MRange[0]));
            Writer.Write(BitConverter.GetBytes(mPoint.MRange[1]));
            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].M)); ;
            }

            return true;
        }
        public bool WriteMultiPointZ(MultiPoint mPoint)
        {
            Writer.Write(enumShapeCode.MultiPointZ);
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(mPoint.BoundingBox.Ymax));
            Writer.Write(BitConverter.GetBytes(mPoint.NumPoints));

            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].X)); ;
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].Y)); ;
            }

            Writer.Write(BitConverter.GetBytes(mPoint.ZRange[0]));
            Writer.Write(BitConverter.GetBytes(mPoint.ZRange[1]));
            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].Z));
            }

            if (!mPoint.HasM) return true;

            Writer.Write(BitConverter.GetBytes(mPoint.MRange[0]));
            Writer.Write(BitConverter.GetBytes(mPoint.MRange[1]));
            for (int i = 0; i < mPoint.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPoint.Points[i].M));
            }

            return true;
        }

        public PolyLine DecodePolyLine(ref byte[] buffer)
        {
            PolyLine line = new PolyLine();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);

            line.BoundingBox = box;
            line.NumParts = BitConverter.ToInt32(buffer, 36);
            line.NumPoints = BitConverter.ToInt32(buffer, 40);
            for (int i = 0; i < line.NumParts; i++)
            {
                line.Parts.Add(BitConverter.ToInt32(buffer, 44 + 4 * i));
            }

            line.Points = new Point[line.NumPoints];
            for (int i = 0; i < line.NumPoints; i++)
            {
                Point pt = new Point();

                pt.X = BitConverter.ToDouble(buffer, 44 + 4 * line.NumParts + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 44 + 4 * line.NumParts + 16 * i + 8);

                line.Points[i] = pt;
            }

            return line;
        }
        public PolyLine DecodePolyLineM(ref byte[] buffer)
        {
            PolyLine line = new PolyLine();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);

            line.BoundingBox = box;
            line.NumParts = BitConverter.ToInt32(buffer, 36);
            line.NumPoints = BitConverter.ToInt32(buffer, 40);
            for (int i = 0; i < line.NumParts; i++)
            {
                line.Parts.Add(BitConverter.ToInt32(buffer, 44 + 4 * i));
            }

            line.Points = new Point[line.NumPoints];
            for (int i = 0; i < line.NumPoints; i++)
            {
                Point pt = new Point();

                pt.X = BitConverter.ToDouble(buffer, 44 + 4 * line.NumParts + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 44 + 4 * line.NumParts + 16 * i + 8);

                line.Points[i] = pt;
            }

            int X = 44 + 4 * line.NumParts;
            int Y = X + 16 * line.NumPoints;

            try
            {
                if (buffer.Length > Y)
                {
                    line.HasM = true;

                    line.MRange[0] = BitConverter.ToDouble(buffer, Y);
                    line.MRange[1] = BitConverter.ToDouble(buffer, Y + 8);
                    for (int i = 0; i < line.NumPoints; i++)
                    {
                        line.Points[i].M = BitConverter.ToDouble(buffer, Y + 16 + 8 * i);
                    }
                }
            }
            catch
            { }

            return line;
        }
        public PolyLine DecodePolyLineZ(ref byte[] buffer)
        {
            PolyLine line = new PolyLine();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);

            line.BoundingBox = box;
            line.NumParts = BitConverter.ToInt32(buffer, 36);
            line.NumPoints = BitConverter.ToInt32(buffer, 40);
            for (int i = 0; i < line.NumParts; i++)
            {
                line.Parts.Add(BitConverter.ToInt32(buffer, 44 + 4 * i));
            }

            line.Points = new Point[line.NumPoints];
            for (int i = 0; i < line.NumPoints; i++)
            {
                Point pt = new Point();

                pt.X = BitConverter.ToDouble(buffer, 44 + 4 * line.NumParts + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 44 + 4 * line.NumParts + 16 * i + 8);

                line.Points[i] = pt;
            }

            int X = 44 + 4 * line.NumParts;
            int Y = X + 16 * line.NumPoints;
            int Z = Y + 16 + 8 * line.NumPoints;

            line.ZRange[0] = BitConverter.ToDouble(buffer, Y);
            line.ZRange[1] = BitConverter.ToDouble(buffer, Y + 8);
            for (int i = 0; i < line.NumPoints; i++)
            {
                line.Points[i].Z = BitConverter.ToDouble(buffer, Y + 16 + 8 * i);
            }

            try
            {
                if (buffer.Length > Z)
                {
                    line.HasM = true;

                    line.MRange[0] = BitConverter.ToDouble(buffer, Z);
                    line.MRange[1] = BitConverter.ToDouble(buffer, Z + 8);
                    for (int i = 0; i < line.NumPoints; i++)
                    {
                        line.Points[i].M = BitConverter.ToDouble(buffer, Z + 16 + 8 * i);
                    }
                }
            }
            catch
            { }

            return line;
        }

        public bool WritePolyLine(PolyLine line)
        {
            Writer.Write(enumShapeCode.Polyline);
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Ymax));

            Writer.Write(BitConverter.GetBytes(line.NumParts));
            Writer.Write(BitConverter.GetBytes(line.NumPoints));

            for (int i = 0; i < line.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Parts[i]));
            }

            for (int i = 0; i < line.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Points[i].X));
                Writer.Write(BitConverter.GetBytes(line.Points[i].Y));
            }

            return true;
        }
        public bool WritePolyLineM(PolyLine line)
        {
            Writer.Write(enumShapeCode.PolylineM);
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Ymax));

            Writer.Write(BitConverter.GetBytes(line.NumParts));
            Writer.Write(BitConverter.GetBytes(line.NumPoints));

            for (int i = 0; i < line.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Parts[i]));
            }

            for (int i = 0; i < line.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Points[i].X));
                Writer.Write(BitConverter.GetBytes(line.Points[i].Y));
            }

            if (!line.HasM) return true;
            Writer.Write(BitConverter.GetBytes(line.MRange[0]));
            Writer.Write(BitConverter.GetBytes(line.MRange[1]));
            for (int i = 0; i < line.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Points[i].M));
            }

            return true;
        }
        public bool WritePolyLineZ(PolyLine line)
        {
            Writer.Write(enumShapeCode.PolylineZ);
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(line.BoundingBox.Ymax));

            Writer.Write(BitConverter.GetBytes(line.NumParts));
            Writer.Write(BitConverter.GetBytes(line.NumPoints));

            for (int i = 0; i < line.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Parts[i]));
            }

            for (int i = 0; i < line.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Points[i].X));
                Writer.Write(BitConverter.GetBytes(line.Points[i].Y));
            }

            Writer.Write(BitConverter.GetBytes(line.ZRange[0]));
            Writer.Write(BitConverter.GetBytes(line.ZRange[1]));
            for (int i = 0; i < line.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Points[i].Z));
            }

            if (!line.HasM) return true;
            Writer.Write(BitConverter.GetBytes(line.MRange[0]));
            Writer.Write(BitConverter.GetBytes(line.MRange[1]));
            for (int i = 0; i < line.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(line.Points[i].M));
            }

            return true;
        }

        public Polygon DecodePolygon(ref byte[] buffer)
        {
            Polygon polygon = new Polygon();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);

            polygon.BoundingBox = box;
            polygon.NumParts = BitConverter.ToInt32(buffer, 36);
            polygon.NumPoints = BitConverter.ToInt32(buffer, 40);
            for (int i = 0; i < polygon.NumParts; i++)
            {
                polygon.Parts.Add(BitConverter.ToInt32(buffer, 44 + 4 * i));
            }

            polygon.Points = new Point[polygon.NumPoints];
            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Point pt = new Point();

                pt.X = BitConverter.ToDouble(buffer, 44 + 4 * polygon.NumParts + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 44 + 4 * polygon.NumParts + 16 * i + 8);

                polygon.Points[i] = pt;
            }

            return polygon;
        }
        public Polygon DecodePolygonM(ref byte[] buffer)
        {
            Polygon polygon = new Polygon();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);

            polygon.BoundingBox = box;
            polygon.NumParts = BitConverter.ToInt32(buffer, 36);
            polygon.NumPoints = BitConverter.ToInt32(buffer, 40);
            for (int i = 0; i < polygon.NumParts; i++)
            {
                polygon.Parts.Add(BitConverter.ToInt32(buffer, 44 + 4 * i));
            }

            polygon.Points = new Point[polygon.NumPoints];
            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Point pt = new Point();

                pt.X = BitConverter.ToDouble(buffer, 44 + 4 * polygon.NumParts + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 44 + 4 * polygon.NumParts + 16 * i + 8);

                polygon.Points[i] = pt;
            }

            int X = 44 + 4 * polygon.NumParts;
            int Y = X + 16 * polygon.NumPoints;

            try
            {
                if (buffer.Length > Y)
                {
                    polygon.HasM = true;

                    polygon.MRange[0] = BitConverter.ToDouble(buffer, Y);
                    polygon.MRange[1] = BitConverter.ToDouble(buffer, Y + 8);
                    for (int i = 0; i < polygon.NumPoints; i++)
                    {
                        polygon.Points[i].M = BitConverter.ToDouble(buffer, Y + 16 + 8 * i);
                    }
                }
            }
            catch
            { }

            return polygon;
        }
        public Polygon DecodePolygonZ(ref byte[] buffer)
        {
            Polygon polygon = new Polygon();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);

            polygon.BoundingBox = box;
            polygon.NumParts = BitConverter.ToInt32(buffer, 36);
            polygon.NumPoints = BitConverter.ToInt32(buffer, 40);
            for (int i = 0; i < polygon.NumParts; i++)
            {
                polygon.Parts.Add(BitConverter.ToInt32(buffer, 44 + 4 * i));
            }

            polygon.Points = new Point[polygon.NumPoints];
            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Point pt = new Point();

                pt.X = BitConverter.ToDouble(buffer, 44 + 4 * polygon.NumParts + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, 44 + 4 * polygon.NumParts + 16 * i + 8);

                polygon.Points[i] = pt;
            }

            int X = 44 + 4 * polygon.NumParts;
            int Y = X + 16 * polygon.NumPoints;
            int Z = Y + 16 + 8 * polygon.NumPoints;

            polygon.ZRange[0] = BitConverter.ToDouble(buffer, Y);
            polygon.ZRange[1] = BitConverter.ToDouble(buffer, Y + 8);
            for (int i = 0; i < polygon.NumPoints; i++)
            {
                polygon.Points[i].Z = BitConverter.ToDouble(buffer, Y + 16 + 8 * i);
            }

            try
            {
                if (buffer.Length > Z)
                {
                    polygon.HasM = true;

                    polygon.MRange[0] = BitConverter.ToDouble(buffer, Z);
                    polygon.MRange[1] = BitConverter.ToDouble(buffer, Z + 8);
                    for (int i = 0; i < polygon.NumPoints; i++)
                    {
                        polygon.Points[i].M = BitConverter.ToDouble(buffer, Z + 16 + 8 * i);
                    }
                }
            }
            catch
            { }

            return polygon;
        }

        public bool WritePolygon(Polygon polygon)
        {
            Writer.Write(enumShapeCode.Polygon);
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Ymax));

            Writer.Write(BitConverter.GetBytes(polygon.NumParts));
            Writer.Write(BitConverter.GetBytes(polygon.NumPoints));

            for (int i = 0; i < polygon.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Parts[i]));
            }

            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].X));
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].Y));
            }

            return true;
        }
        public bool WritePolygonM(Polygon polygon)
        {
            Writer.Write(enumShapeCode.PolygonM);
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Ymax));

            Writer.Write(BitConverter.GetBytes(polygon.NumParts));
            Writer.Write(BitConverter.GetBytes(polygon.NumPoints));

            for (int i = 0; i < polygon.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Parts[i]));
            }

            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].X));
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].Y));
            }

            if (!polygon.HasM) return true;
            Writer.Write(BitConverter.GetBytes(polygon.MRange[0]));
            Writer.Write(BitConverter.GetBytes(polygon.MRange[1]));
            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].M));
            }

            return true;
        }
        public bool WritePolygonZ(Polygon polygon)
        {
            Writer.Write(enumShapeCode.PolygonZ);
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(polygon.BoundingBox.Ymax));

            Writer.Write(BitConverter.GetBytes(polygon.NumParts));
            Writer.Write(BitConverter.GetBytes(polygon.NumPoints));

            for (int i = 0; i < polygon.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Parts[i]));
            }

            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].X));
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].Y));
            }

            Writer.Write(BitConverter.GetBytes(polygon.ZRange[0]));
            Writer.Write(BitConverter.GetBytes(polygon.ZRange[1]));
            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].Z));
            }

            if (!polygon.HasM) return true;
            Writer.Write(BitConverter.GetBytes(polygon.MRange[0]));
            Writer.Write(BitConverter.GetBytes(polygon.MRange[1]));
            for (int i = 0; i < polygon.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(polygon.Points[i].M));
            }

            return true;
        }

        public MultiPatch DecodeMultipatch(ref byte[] buffer)
        {
            MultiPatch mPatch = new MultiPatch();
            Box box = new Box();

            box.Xmin = BitConverter.ToDouble(buffer, 4);
            box.Ymin = BitConverter.ToDouble(buffer, 12);
            box.Xmax = BitConverter.ToDouble(buffer, 20);
            box.Ymax = BitConverter.ToDouble(buffer, 28);
            mPatch.BoundingBox = box;

            mPatch.NumParts = BitConverter.ToInt32(buffer, 36);
            mPatch.NumPoints = BitConverter.ToInt32(buffer, 40);

            int W = 44 + 4 * mPatch.NumParts;
            int X = W + 4 * mPatch.NumParts;
            int Y = X + 16 * mPatch.NumPoints;
            int Z = Y + 16 + 8 * mPatch.NumPoints;

            for (int i = 0; i < mPatch.NumParts; i++)
            {
                mPatch.Parts.Add(BitConverter.ToInt32(buffer, 44 + i * 4));
            }

            for (int i = 0; i < mPatch.NumParts; i++)
            {
                mPatch.PartTypes.Add((enumPartType)BitConverter.ToInt32(buffer, W + i * 4));
            }

            for (int i = 0; i < mPatch.NumPoints; i++)
            {
                Point pt = new Point();

                pt.X = BitConverter.ToDouble(buffer, X + 16 * i);
                pt.Y = BitConverter.ToDouble(buffer, X + 16 * i + 8);
                mPatch.Points[i] = pt;
            }

            mPatch.ZRange[0] = BitConverter.ToDouble(buffer, Y);
            mPatch.ZRange[1] = BitConverter.ToDouble(buffer, Y + 8);
            for (int i = 0; i < mPatch.NumPoints; i++)
            {
                mPatch.ZArray[i] = BitConverter.ToDouble(buffer, Y + 16 + 8 * i);
            }

            if (buffer.Length > Z)
            {
                mPatch.HasM = true;

                mPatch.MRange[0] = BitConverter.ToDouble(buffer, Z);
                mPatch.MRange[1] = BitConverter.ToDouble(buffer, Z + 8);
                for (int i = 0; i < mPatch.NumPoints; i++)
                {
                    mPatch.MArray[i] = BitConverter.ToDouble(buffer, Z + 16 + 8 * i);
                }
            }

            return mPatch;
        }
        public bool WriteMultiPatch(MultiPatch mPatch)
        {
            Writer.Write(enumShapeCode.MultiPatch);
            Writer.Write(BitConverter.GetBytes(mPatch.BoundingBox.Xmin));
            Writer.Write(BitConverter.GetBytes(mPatch.BoundingBox.Ymin));
            Writer.Write(BitConverter.GetBytes(mPatch.BoundingBox.Xmax));
            Writer.Write(BitConverter.GetBytes(mPatch.BoundingBox.Ymax));

            Writer.Write(BitConverter.GetBytes(mPatch.NumParts));
            Writer.Write(BitConverter.GetBytes(mPatch.NumPoints));

            for (int i = 0; i < mPatch.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPatch.Parts[i]));
            }

            for (int i = 0; i < mPatch.NumParts; i++)
            {
                Writer.Write(BitConverter.GetBytes((int)(mPatch.PartTypes[i])));
            }

            for (int i = 0; i < mPatch.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPatch.Points[i].X));
                Writer.Write(BitConverter.GetBytes(mPatch.Points[i].Y));
            }

            Writer.Write(BitConverter.GetBytes(mPatch.ZRange[0]));
            Writer.Write(BitConverter.GetBytes(mPatch.ZRange[1]));
            for (int i = 0; i < mPatch.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPatch.Points[i].Z));
            }

            if (!mPatch.HasM) return true;
            Writer.Write(BitConverter.GetBytes(mPatch.MRange[0]));
            Writer.Write(BitConverter.GetBytes(mPatch.MRange[1]));
            for (int i = 0; i < mPatch.NumPoints; i++)
            {
                Writer.Write(BitConverter.GetBytes(mPatch.Points[i].M));
            }

            return true;
        }
    }
}
