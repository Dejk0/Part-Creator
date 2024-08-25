using SolidEdgeConstants;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidEdgeConstants;
using System.Runtime.InteropServices;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Part_Creator
{
    public class SE_DT
    {
        public SolidEdgeFramework.Application application = null;
        public SolidEdgeFramework.Documents documents = null;
        public SolidEdgePart.PartDocument part = null;
        public SolidEdgePart.ProfileSets profileSets = null;
        public SolidEdgePart.ProfileSet profileSet = null;
        public SolidEdgePart.Profiles profiles = null;
        public SolidEdgePart.Profile profile = null;
        public SolidEdgePart.Profile profile2 = null;
        public SolidEdgePart.Profile profile3 = null;
        public SolidEdgePart.RefPlanes refplanes = null;
        public SolidEdgePart.RefPlanes planesCollection = null;
        public SolidEdgePart.RefPlane plane = null;
        public SolidEdgePart.ExtrudedCutouts extrudedCutouts = null;
        public SolidEdgeFrameworkSupport.Relations2d relations2d = null;
        public SolidEdgeFrameworkSupport.Relation2d relation2d = null;
        public SolidEdgeFrameworkSupport.Lines2d lines2d = null;
        public SolidEdgeFrameworkSupport.Line2d line2d = null;
        public SolidEdgePart.Models models = null;
        public SolidEdgePart.Model model = null;
        public SolidEdgePart.Model model2 = null;
        public System.Array aProfiles = null;
        public System.Array bProfiles = null;
        public System.Array cProfiles = null;
        public SolidEdgePart.RefAxes axes = null;
        public SolidEdgePart.RefAxis axis = null;
        public SolidEdgePart.Arcs3D arc3d = null;
        public float x_Coordinate;
        public float y_Coordinate;
        public float z_Coordinate;
        



        public SE_DT()
        {
            // Itt hozzuk létre a Solid Edge alkalmazás példányát, amit később használni fogunk.
            application = (SolidEdgeFramework.Application)Marshal.GetActiveObject("SolidEdge.Application");
        }
    
        public void CreatingRelations(SolidEdgeFrameworkSupport.Relations2d relations2dToCreat, SolidEdgeFrameworkSupport.Relation2d relation2dToCreat, SolidEdgePart.Profile profileForRelations)
        {
            // Define Relations among the Line objects to make the Profile closed
            relations2dToCreat = (SolidEdgeFrameworkSupport.Relations2d)
              profileForRelations.Relations2d;
            for (int i = 0; i < lines2d.Count - 1; i++)
            {
                relation2dToCreat = relations2dToCreat.AddKeypoint(
             lines2d.Item(i + 1),
             (int)KeypointIndexConstants.igLineEnd,
             lines2d.Item(i + 2),
             (int)KeypointIndexConstants.igLineStart,
             true);
            }
            relation2dToCreat = relations2dToCreat.AddKeypoint(
             lines2d.Item(lines2d.Count),
             (int)KeypointIndexConstants.igLineEnd,
             lines2d.Item(1),
             (int)KeypointIndexConstants.igLineStart,
             true);

        }

        
        public void Cube(double x, double y, double z)
        {
            
            lines2d.AddBy2Points(x_Coordinate, x_Coordinate, x_Coordinate + x / 1000, y_Coordinate);
            lines2d.AddBy2Points(x_Coordinate + x / 1000, y_Coordinate, x_Coordinate + x / 1000, y_Coordinate + y / 1000);
            lines2d.AddBy2Points(x_Coordinate + x / 1000, y_Coordinate + y / 1000, x_Coordinate, y_Coordinate + y / 1000);
            lines2d.AddBy2Points(x_Coordinate, y_Coordinate + y / 1000, x_Coordinate, y_Coordinate);
            CreatingRelations(relations2d,relation2d,profile);
            CloseProfile(profile);
            // Hide the profile
            profile.Visible = false;
            CreatingTheProfile(aProfiles,profile);
            ModellReferenc(models,part);
            Extrude(200);
        }
        public void StartingSetup()
        {
            // Get a reference to the profile sets collection
            profileSets = part.ProfileSets;

            // Add a new profile set
            profileSet = profileSets.Add();

            // Get a reference to the profiles collection
            profiles = profileSet.Profiles;

            // Get a reference to the ref planes collection
            refplanes = part.RefPlanes;
        }
        public SolidEdgePart.Profile StartingData1( SolidEdgePart.Profile profileRef, int plane, SolidEdgePart.RefPlanes planesRef, SolidEdgePart.Profiles profilesRef)
        {

           return profileRef = profilesRef.Add(planesRef.Item(plane));


        }
        public SolidEdgeFrameworkSupport.Lines2d StartingData2(SolidEdgePart.Profile profileRef, SolidEdgeFrameworkSupport.Lines2d lines2DRef)
        {

            // Get a reference to the lines2d collection
            return lines2DRef = profileRef.Lines2d;

        }
        public void CloseProfile(SolidEdgePart.Profile profileToClose)
        {
            // Close the profile
            profileToClose.End(SolidEdgePart.ProfileValidationType.igProfileClosed);
        }
        public System.Array CreatingTheProfile(System.Array arrayToCreatProfile, SolidEdgePart.Profile profileToCreat)
        {
            // Create a new array of profile objects
            arrayToCreatProfile = Array.CreateInstance(typeof(SolidEdgePart.Profile), 1);
            arrayToCreatProfile.SetValue(profileToCreat, 0);
            return arrayToCreatProfile;
        }
        public SolidEdgePart.Models ModellReferenc(SolidEdgePart.Models modelsToRef, SolidEdgePart.PartDocument partToRef)
        {
            return modelsToRef = partToRef.Models;
        }
        public void Extrude(double extrude)
        {
            // Create the extended protrusion.
            model = models.AddFiniteExtrudedProtrusion(
              aProfiles.Length,
              ref aProfiles,
              SolidEdgePart.FeaturePropertyConstants.igRight,
              extrude / 1000,
              Missing.Value,
              Missing.Value,
              Missing.Value,
              Missing.Value);
        }
        public SolidEdgePart.RefAxis GetAxis(SolidEdgeFrameworkSupport.Lines2d lines2DToAxis, SolidEdgePart.RefAxis axisToRef, SolidEdgePart.Profile profileToAxis)
        {
            int count = lines2DToAxis.Count;
            axisToRef = (SolidEdgePart.RefAxis)profileToAxis.SetAxisOfRevolution(
                LineForAxis: lines2DToAxis.Item(count));
            return axisToRef;
        }
        public SolidEdgePart.Model Revolve(SolidEdgePart.Models modelsToRef, System.Array arrayToRef, SolidEdgePart.RefAxis axisToRef)
        {
            //SolidEdgePart.Model modelToRef, System.Array arrayToRef, SolidEdgePart.RefAxis axisToRef
            return models.AddFiniteRevolvedProtrusion(
                NumberOfProfiles: 1,
                ProfileArray: aProfiles,
                ReferenceAxis: axis,
                ProfilePlaneSide: SolidEdgePart.FeaturePropertyConstants.igRight,
                AngleofRevolution: 2 * Math.PI
                );
        }
        public void Shaft(double diameter1, double lenght1, double diameter2, double lenght2, double diameter3, double lenght3)
        {
            double lenght_1 = lenght1 / 1000;
            double lenght_2 = (lenght1 + lenght2) / 1000;
            double lenght_3 = (lenght3 + lenght1 + lenght2) / 1000;

            diameter1 = diameter1 / 2000;
            diameter2 = diameter2 / 2000;
            diameter3 = diameter3 / 2000;



            List<string[]> csvDataList = Part_Creator.Form1.csvDataList;
            double d1 = double.Parse(csvDataList[Part_Creator.Form1.atm1][0]) / 1000;
            double s = double.Parse(csvDataList[Part_Creator.Form1.atm1][1]) / 1000;
            double a = double.Parse(csvDataList[Part_Creator.Form1.atm1][2]) / 1000;
            double b = double.Parse(csvDataList[Part_Creator.Form1.atm1][3]) / 1000;
            double d2 = double.Parse(csvDataList[Part_Creator.Form1.atm1][4]) / 2000;
            double m = double.Parse(csvDataList[Part_Creator.Form1.atm1][5]) / 1000;
            double n = double.Parse(csvDataList[Part_Creator.Form1.atm1][6]) / 1000;

            double d12 = double.Parse(csvDataList[Part_Creator.Form1.atm2][0]) / 1000;
            double s2 = double.Parse(csvDataList[Part_Creator.Form1.atm2][1]) / 1000;
            double a2 = double.Parse(csvDataList[Part_Creator.Form1.atm2][2]) / 1000;
            double b2 = double.Parse(csvDataList[Part_Creator.Form1.atm2][3]) / 1000;
            double d22 = double.Parse(csvDataList[Part_Creator.Form1.atm2][4]) / 2000;
            double m2 = double.Parse(csvDataList[Part_Creator.Form1.atm2][5]) / 1000;
            double n2 = double.Parse(csvDataList[Part_Creator.Form1.atm2][6]) / 1000;

            double chamfer1 = Part_Creator.Form1.chamfer1 / 1000;
            double chamfer2 = Part_Creator.Form1.chamfer2 / 1000;
            List<double[]> coordinates = new List<double[]>();

            coordinates.Add(new double[] { 0, 0, 0, diameter1 - chamfer1 });

            coordinates.Add(new double[] { 0, diameter1 - chamfer1, chamfer1, diameter1 });
            coordinates.Add(new double[] { chamfer1, diameter1, n, diameter1 }); // első letörés kész
            coordinates.Add(new double[] { n, diameter1, n, d2 });
            coordinates.Add(new double[] { n, d2, n + m, d2 });
            coordinates.Add(new double[] { n + m, d2, n + m, diameter1 });
            coordinates.Add(new double[] { n + m, diameter1, lenght_1, diameter1 });//első válll kész

            coordinates.Add(new double[] { lenght_1, diameter1, lenght_1, diameter2 });
            coordinates.Add(new double[] { lenght_1, diameter2, lenght_2, diameter2 });
            coordinates.Add(new double[] { lenght_2, diameter2, lenght_2, diameter3 }); //második válll kész

            coordinates.Add(new double[] { lenght_2, diameter3, lenght_3 - (n2 + m2), diameter3 });
            coordinates.Add(new double[] { lenght_3 - (n2 + m2), diameter3, lenght_3 - (n2 + m2), d22 });
            coordinates.Add(new double[] { lenght_3 - (n2 + m2), d22, lenght_3 - (n2), d22 });
            coordinates.Add(new double[] { lenght_3 - (n2), d22, lenght_3 - (n2), diameter3 });
            coordinates.Add(new double[] { lenght_3 - (n2), diameter3, lenght_3 - chamfer2, diameter3 });
            coordinates.Add(new double[] { lenght_3 - chamfer2, diameter3, lenght_3, diameter3 - chamfer2 });
            coordinates.Add(new double[] { lenght_3, diameter3 - chamfer2, lenght_3, 0 });//haramdik váll kész /második letörés kész

            coordinates.Add(new double[] { lenght_3, 0, 00, 0 });
            StartingSetup();
            profile = StartingData1(profile,3,refplanes,profiles);
            lines2d = StartingData2(profile, lines2d);
            for (int i = 0; i < coordinates.Count; i++)
            {
                lines2d.AddBy2Points(coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]);
            }


            CreatingRelations(relations2d,relation2d,profile);
            axis = GetAxis(lines2d,axis,profile);
            CloseProfile(profile);
            profile.Visible = true;
            aProfiles = CreatingTheProfile(aProfiles,profile);
            models = ModellReferenc(models,part);
            model = Revolve(models,aProfiles,axis);
            resetreffs();

            plane = CreatPlane(planesCollection, plane, part, diameter1 * 2000);
            StartingSetup();
            plane.Visible = false;
            profile2 = StartingData1(profile, 4, refplanes, profiles);
            lines2d = StartingData2(profile2, lines2d);
            latch1(20, 40, 8,5.3, n+m+2.0/1000);

            plane = CreatPlane(planesCollection, plane, part, diameter3 * 2000);
            StartingSetup();
            plane.Visible = false;
            profile3 = StartingData1(profile, 5, refplanes, profiles);
            lines2d = StartingData2(profile3, lines2d);          
            latch2(20, 40, 8, 5.3, lenght_3-n2-m2-2.0/1000);
            //model2.Rounds.Add(1, lines2d.Item(1), lines2d.Item(0) );
            profile2.Visible = false;
            profile3.Visible = false;
        }
        public void resetreffs()
        {
            SolidEdgePart.ProfileSets profileSets = null;
            SolidEdgePart.ProfileSet profileSet = null;
            SolidEdgePart.Profiles profiles = null;
            SolidEdgePart.Profile profile = null;
            SolidEdgePart.RefPlanes refplanes = null;
            relations2d = null;
            relation2d = null;
            lines2d = null;
            line2d = null;
            profile = null;
        }
        public SolidEdgePart.RefPlane CreatPlane(SolidEdgePart.RefPlanes refPlanesToCreat, SolidEdgePart.RefPlane refPlaneToCreat, SolidEdgePart.PartDocument partref, double atm)
        {
            refPlanesToCreat = partref.RefPlanes;
            return refPlaneToCreat = refPlanesToCreat.AddParallelByDistance(ParentPlane: refPlanesToCreat.Item(1),
                Distance: atm / 2000,
                NormalSide: SolidEdgePart.ReferenceElementConstants.igNormalSide,
                Local: false);
        }

        public void latch1(double atm, double b, double h1, double z, double shaftend)
        {
           
            List<double[]> coordinates = new List<double[]>();

            coordinates.Add(new double[] { shaftend, h1/2000, shaftend + b/1000, h1/2000 });
            coordinates.Add(new double[] { shaftend + b / 1000, h1 / 2000, shaftend + b / 1000, -h1 / 2000 });
            coordinates.Add(new double[] { shaftend + b / 1000, -h1 / 2000, shaftend, -h1 / 2000 });
            coordinates.Add(new double[] { shaftend, -h1 / 2000, shaftend, h1 / 2000 });

            for (int i = 0; i < coordinates.Count; i++)
            {
                lines2d.AddBy2Points(coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]);
            }

            CreatingRelations(relations2d,relation2d,profile2);

            profile2.End(SolidEdgePart.ProfileValidationType.igProfileClosed);

            bProfiles = Array.CreateInstance(typeof(SolidEdgePart.Profile), 1);
            bProfiles.SetValue(profile2, 0);


            extrudedCutouts = model.ExtrudedCutouts;

            extrudedCutouts.AddFinite(
                Profile: profile2,
                ProfileSide: SolidEdgePart.FeaturePropertyConstants.igRight,
                ProfilePlaneSide: SolidEdgePart.FeaturePropertyConstants.igLeft,
                Depth: z/1000
                );


        }
        public void latch2(double atm, double b, double h1, double z, double shaftend)
        {
            List<double[]> coordinates = new List<double[]>();
            coordinates.Add(new double[] { shaftend, h1 / 2000, shaftend - b / 1000, h1 / 2000 });
            coordinates.Add(new double[] { shaftend - b / 1000, h1 / 2000, shaftend- b / 1000, -h1 / 2000 });
            coordinates.Add(new double[] { shaftend  - b / 1000, - h1 / 2000, shaftend, -h1 / 2000 });
            coordinates.Add(new double[] { shaftend, -h1 / 2000, shaftend, h1 / 2000 });

            for (int i = 0; i < coordinates.Count; i++)
            {
                lines2d.AddBy2Points(coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]);
            }

            CreatingRelations(relations2d, relation2d, profile3);

            profile3.End(SolidEdgePart.ProfileValidationType.igProfileClosed);

            cProfiles = Array.CreateInstance(typeof(SolidEdgePart.Profile), 1);
            cProfiles.SetValue(profile3, 0);


            extrudedCutouts = model.ExtrudedCutouts;

            extrudedCutouts.AddFinite(
                Profile: profile3,
                ProfileSide: SolidEdgePart.FeaturePropertyConstants.igLeft,
                ProfilePlaneSide: SolidEdgePart.FeaturePropertyConstants.igLeft,
                Depth: z / 1000
                );


        }
    }
}