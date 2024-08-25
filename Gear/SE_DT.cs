using SolidEdgeConstants;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Gear
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
    SolidEdgeFrameworkSupport.Arcs2d arcs2d = null;
    SolidEdgeFrameworkSupport.Arc2d arc2d = null;
    public SolidEdgeFrameworkSupport.BSplineCurves2d bSplineCurves2d = null;
    public SolidEdgeFrameworkSupport.BSplineCurve2d bSplineCurve2d = null;
    public SolidEdgeFramework.LinearStyles linearStyles = null;
    public SolidEdgeFramework.LinearStyle linearStyle = null;

    public SE_DT()
    {
      application = (SolidEdgeFramework.Application)Marshal.GetActiveObject("SolidEdge.Application");
    }

    public void CreatingRelations(SolidEdgeFrameworkSupport.Relations2d relations2dToCreat, SolidEdgeFrameworkSupport.Relation2d relation2dToCreat, SolidEdgePart.Profile profileForRelations)
    {
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
      CreatingRelations(relations2d, relation2d, profile);
      CloseProfile(profile);
      // Hide the profile
      profile.Visible = false;
      CreatingTheProfile(aProfiles, profile);
      ModellReferenc(models, part);
      Extrude(200);
    }
    public void StartingSetup()
    {
      profileSets = part.ProfileSets;
      profileSet = profileSets.Add();
      profiles = profileSet.Profiles;
      refplanes = part.RefPlanes;
    }
    public SolidEdgePart.Profile StartingData1(SolidEdgePart.Profile profileRef, int plane, SolidEdgePart.RefPlanes planesRef, SolidEdgePart.Profiles profilesRef)
    {

      return profileRef = profilesRef.Add(planesRef.Item(plane));
      ;

    }
    public SolidEdgeFrameworkSupport.Lines2d StartingData2(SolidEdgePart.Profile profileRef, SolidEdgeFrameworkSupport.Lines2d lines2DRef)
    {
      return lines2DRef = profileRef.Lines2d;
    }
    public void CloseProfile(SolidEdgePart.Profile profileToClose)
    {
      profileToClose.End(SolidEdgePart.ProfileValidationType.igProfileClosed);
    }
    public System.Array CreatingTheProfile(System.Array arrayToCreatProfile, SolidEdgePart.Profile profileToCreat)
    {
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



            List<string[]> csvDataList = global::Gear.Gear.csvDataList;
      double d1 = double.Parse(csvDataList[global::Gear.Gear.atm1][0]) / 1000;
      double s = double.Parse(csvDataList[global::Gear.Gear.atm1][1]) / 1000;
      double a = double.Parse(csvDataList[global::Gear.Gear.atm1][2]) / 1000;
      double b = double.Parse(csvDataList[global::Gear.Gear.atm1][3]) / 1000;
      double d2 = double.Parse(csvDataList[global::Gear.Gear.atm1][4]) / 2000;
      double m = double.Parse(csvDataList[global::Gear.Gear.atm1][5]) / 1000;
      double n = double.Parse(csvDataList[global::Gear.Gear.atm1][6]) / 1000;

      double d12 = double.Parse(csvDataList[global::Gear.Gear.atm2][0]) / 1000;
      double s2 = double.Parse(csvDataList[global::Gear.Gear.atm2][1]) / 1000;
      double a2 = double.Parse(csvDataList[global::Gear.Gear.atm2][2]) / 1000;
      double b2 = double.Parse(csvDataList[global::Gear.Gear.atm2][3]) / 1000;
      double d22 = double.Parse(csvDataList[global::Gear.Gear.atm2][4]) / 2000;
      double m2 = double.Parse(csvDataList[global::Gear.Gear.atm2][5]) / 1000;
      double n2 = double.Parse(csvDataList[global::Gear.Gear.atm2][6]) / 1000;

      double chamfer1 = global::Gear.Gear.chamfer1 / 1000;
      double chamfer2 = global::Gear.Gear.chamfer2 / 1000;
      List<double[]> coordinates = new List<double[]>();

      coordinates.Add(new double[] { 0, 0, 0, diameter1 - chamfer1 });

      coordinates.Add(new double[] { 0, diameter1 - chamfer1, chamfer1, diameter1 });
      coordinates.Add(new double[] { chamfer1, diameter1, n, diameter1 }); 
      coordinates.Add(new double[] { n, diameter1, lenght_1, diameter1 });

      coordinates.Add(new double[] { lenght_1, diameter1, lenght_1, diameter2 });
      coordinates.Add(new double[] { lenght_1, diameter2, lenght_2, diameter2 });
      coordinates.Add(new double[] { lenght_2, diameter2, lenght_2, diameter3 }); 

      coordinates.Add(new double[] { lenght_2, diameter3, lenght_3 - (n2), diameter3 });
      coordinates.Add(new double[] { lenght_3 - (n2), diameter3, lenght_3 - chamfer2, diameter3 });
      coordinates.Add(new double[] { lenght_3 - chamfer2, diameter3, lenght_3, diameter3 - chamfer2 });
      coordinates.Add(new double[] { lenght_3, diameter3 - chamfer2, lenght_3, 0 });

      coordinates.Add(new double[] { lenght_3, 0, 00, 0 });
      StartingSetup();
      profile = StartingData1(profile, 3, refplanes, profiles);
      lines2d = StartingData2(profile, lines2d);
      for (int i = 0; i < coordinates.Count; i++)
      {
        lines2d.AddBy2Points(coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]);
      }

      CreatingRelations(relations2d, relation2d, profile);
      axis = GetAxis(lines2d, axis, profile);
      CloseProfile(profile);
      profile.Visible = false;
      aProfiles = CreatingTheProfile(aProfiles, profile);
      models = ModellReferenc(models, part);
      model = Revolve(models, aProfiles, axis);
      resetreffs();

      if (global::Gear.Gear.retesz2)
      {
                plane = CreatPlane(planesCollection, plane, part, diameter1 * 2000);
                StartingSetup();
                plane.Visible = false;
                profile2 = StartingData1(profile, 4, refplanes, profiles);
                lines2d = StartingData2(profile2, lines2d);
                latch1(global::Gear.Gear.retesz1_Hossz, global::Gear.Gear.retesz1_Szelesseg, global::Gear.Gear.retesz1_Magassag, n + m + 2.0 / 1000);
                profile2.Visible = false;
      }
      if (global::Gear.Gear.retesz1)
      {
                plane = CreatPlane(planesCollection, plane, part, diameter3 * 2000);
                StartingSetup();
                plane.Visible = false;
                profile3 = StartingData1(profile, 4, refplanes, profiles);
                lines2d = StartingData2(profile3, lines2d);
                latch2(global::Gear.Gear.retesz2_Hossz, global::Gear.Gear.retesz2_Szelesseg, global::Gear.Gear.retesz2_Magassag, lenght_3 - n2 - m2 - 2.0 / 1000);
                //model2.Rounds.Add(1, lines2d.Item(1), lines2d.Item(0) );  
                profile3.Visible = false;
      }
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
      return refPlaneToCreat = refPlanesToCreat.AddParallelByDistance(
          ParentPlane: refPlanesToCreat.Item(1),
          Distance: atm / 2000,
          NormalSide: SolidEdgePart.ReferenceElementConstants.igNormalSide,
          Local: false);
    }
    public SolidEdgePart.RefPlane CreatPlane2(SolidEdgePart.RefPlanes refPlanesToCreat, SolidEdgePart.RefPlane refPlaneToCreat, SolidEdgePart.PartDocument partref, double atm)
    {
      refPlanesToCreat = partref.RefPlanes;
      return refPlaneToCreat = refPlanesToCreat.AddParallelByDistance(
          ParentPlane: refPlanesToCreat.Item(2),
          Distance: atm / 1000,
          NormalSide: SolidEdgePart.ReferenceElementConstants.igNormalSide,
          Local: false);
    }

    public void latch1(double b, double h1, double z, double shaftend)
    {
      List<double[]> coordinates = new List<double[]>();

      coordinates.Add(new double[] { shaftend + h1 / 2000, h1 / 2000, shaftend + b / 1000 - h1 / 2000, h1 / 2000 });
      coordinates.Add(new double[] { shaftend + b / 1000 - h1 / 2000, h1 / 2000, shaftend + b / 1000 - h1 / 2000, -h1 / 2000 });
      coordinates.Add(new double[] { shaftend + b / 1000 - h1 / 2000, -h1 / 2000, shaftend + h1 / 2000, -h1 / 2000 });
      coordinates.Add(new double[] { shaftend + h1 / 2000, -h1 / 2000, shaftend + h1 / 2000, h1 / 2000 });

      arcs2d = profile2.Arcs2d;
      for (int i = 0; i < coordinates.Count; i = i + 2)
      {
        lines2d.AddBy2Points(coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]);
      }
      for (int i = 1; i < coordinates.Count; i = i + 2)
      {
        arc2d = arcs2d.AddByCenterStartEnd(coordinates[i][2], 0, coordinates[i][2], coordinates[i][3], coordinates[i][0], coordinates[i][1]);
      }
      CreatingRelations3(relations2d, relation2d, profile2);
      profile2.End(SolidEdgePart.ProfileValidationType.igProfileClosed);
      bProfiles = Array.CreateInstance(typeof(SolidEdgePart.Profile), 1);
      bProfiles.SetValue(profile2, 0);
      extrudedCutouts = model.ExtrudedCutouts;
      extrudedCutouts.AddFinite(
          Profile: profile2,
          ProfileSide: SolidEdgePart.FeaturePropertyConstants.igRight,
          ProfilePlaneSide: SolidEdgePart.FeaturePropertyConstants.igLeft,
          Depth: z / 1000
          );
    }
    public void latch2(double b, double h1, double z, double shaftend)
    {
      List<double[]> coordinates = new List<double[]>();
      coordinates.Add(new double[] { shaftend - h1 / 2000, h1 / 2000, shaftend - b / 1000 + h1 / 2000, h1 / 2000 });
      coordinates.Add(new double[] { shaftend - b / 1000 + h1 / 2000, h1 / 2000, shaftend - b / 1000 + h1 / 2000, -h1 / 2000 });
      coordinates.Add(new double[] { shaftend - b / 1000 + h1 / 2000, -h1 / 2000, shaftend - h1 / 2000, -h1 / 2000 });
      coordinates.Add(new double[] { shaftend - h1 / 2000, -h1 / 2000, shaftend - h1 / 2000, h1 / 2000 });

      arcs2d = profile3.Arcs2d;
      for (int i = 0; i < coordinates.Count; i = i + 2)
      {
        lines2d.AddBy2Points(coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]);
      }
      for (int i = coordinates.Count-1; i > 0; i = i - 2)
      {
        arc2d = arcs2d.AddByCenterStartEnd(coordinates[i][0], 0, coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]);
      }

      CreatingRelations3(relations2d, relation2d, profile3);
      profile3.End(SolidEdgePart.ProfileValidationType.igProfileClosed);
      cProfiles = Array.CreateInstance(typeof(SolidEdgePart.Profile), 1);
      cProfiles.SetValue(profile3, 0);
      extrudedCutouts = model.ExtrudedCutouts;
      extrudedCutouts.AddFinite(
          Profile: profile3,
          ProfileSide: SolidEdgePart.FeaturePropertyConstants.igRight,
          ProfilePlaneSide: SolidEdgePart.FeaturePropertyConstants.igLeft,
          Depth: z / 1000
          );
    }
    public void Fogaskerek(double kihuzas, double poz)
    {
      double d_k = global::Gear.Gear.fejkor / 1000;
      double d_b = global::Gear.Gear.alapkor / 1000;
      double fogakszam = global::Gear.Gear.fogszam;
      double fejmagassag = global::Gear.Gear.fogmagassag / 1000;

      double fogosztás = (360.0 / fogakszam / 2) * Math.PI / 180;
      double alfa = Math.Acos(d_b / d_k);
      double ialfa = Math.Tan(alfa) - (alfa);
      double alfa_max = (alfa + ialfa);
      double alámetszés = d_b / 2 - (d_k / 2 - fejmagassag);
      double alfa_talp = Math.Acos(d_b / (d_b + alámetszés * 2));
      double alámetszésívhossz = (2 * Math.PI * (d_b / 2) / 360) * (alfa_talp * 180 / Math.PI);
      double labkor = ((d_b / 2) - alámetszés);
      double osztasokszam = 3;
      double forgatas = 0;
      List<double[]> coordinates = new List<double[]>();
      double r_evolv;
      double beta_talp = Math.Tan(alfa_talp) - alfa_talp;
      double x_post = labkor * Math.Sin(beta_talp);
      double y_post = labkor * Math.Cos(beta_talp);

      for (double j = 0; j < fogakszam; j++)
      {
        r_evolv = d_b / 2;
        double r_evolv_2 = d_b / (2 * Math.Cos(alfa_talp));
        double szog1 = Math.Tan(alfa_talp) - alfa_talp;
        double szog2 = Math.Tan(alfa) - alfa;
        double x = r_evolv * Math.Sin(forgatas);
        double y = r_evolv * Math.Cos(forgatas);
        coordinates.Add(new double[] { x_post, y_post, x, y });
        x_post = x;
        y_post = y;
        r_evolv = d_k / 2;
        x = r_evolv * Math.Sin(szog2 + forgatas);
        y = r_evolv * Math.Cos(szog2 + forgatas);
        coordinates.Add(new double[] { x_post, y_post, x, y });
        x_post = x;
        y_post = y;
        x = r_evolv * Math.Sin(-szog2 + fogosztás + forgatas);
        y = r_evolv * Math.Cos(-szog2 + fogosztás + forgatas);
        coordinates.Add(new double[] { x_post, y_post, x, y });
        x_post = x;
        y_post = y;
        r_evolv = d_b / 2;
        x = r_evolv * Math.Sin(fogosztás + forgatas);
        y = r_evolv * Math.Cos(fogosztás + forgatas);
        coordinates.Add(new double[] { x_post, y_post, x, y });
        x_post = x;
        y_post = y;
        r_evolv = labkor;
        x = r_evolv * Math.Sin(-szog1 + fogosztás + forgatas);
        y = r_evolv * Math.Cos(-szog1 + fogosztás + forgatas);
        coordinates.Add(new double[] { x_post, y_post, x, y });
        x_post = x;
        y_post = y;
        x = r_evolv * Math.Sin(2 * fogosztás + forgatas);
        y = r_evolv * Math.Cos(2 * fogosztás + forgatas);
        coordinates.Add(new double[] { x_post, y_post, x, y });
        x_post = x;
        y_post = y;
        forgatas = forgatas + 2 * fogosztás;
      }

      plane = CreatPlane2(planesCollection, plane, part, poz);
      StartingSetup();
      plane.Visible = false;
      if (refplanes.Count > 4)
      {
        profile = StartingData1(profile, 5, refplanes, profiles);
      }
      else
      { profile = StartingData1(profile, refplanes.Count, refplanes, profiles); }

      lines2d = StartingData2(profile, lines2d);
      linearStyles = (SolidEdgeFramework.LinearStyles)part.LinearStyles;
      linearStyle = linearStyles.Item(1);
      bSplineCurves2d = profile.BSplineCurves2d;
      arcs2d = profile.Arcs2d;
      x_post = labkor * Math.Sin(beta_talp);
      y_post = labkor * Math.Cos(beta_talp);
      arc2d = arcs2d.AddByCenterStartEnd(0, 0, x_post, y_post, 0, labkor);
      for (int j = 0; j < fogakszam; j++)
      {
        var Points = (System.Array)new double[] { coordinates[6 * j][0], coordinates[6 * j][1], coordinates[6 * j + 1][0], coordinates[6 * j + 1][1], coordinates[6 * j + 2][0], coordinates[6 * j + 2][1] };
        bSplineCurve2d = bSplineCurves2d.AddByPoints(3, Points.Length / 2, ref Points);
        arc2d = arcs2d.AddByCenterStartEnd(0, 0, coordinates[6 * j + 3][0], coordinates[6 * j + 3][1], coordinates[6 * j + 2][0], coordinates[6 * j + 2][1]);
        Points = (System.Array)new double[] { coordinates[6 * j + 3][0], coordinates[6 * j + 3][1], coordinates[6 * j + 1 + 3][0], coordinates[6 * j + 1 + 3][1], coordinates[6 * j + 2 + 3][0], coordinates[6 * j + 2 + 3][1] };
        bSplineCurve2d = bSplineCurves2d.AddByPoints(3, Points.Length / 2, ref Points);
        arc2d = arcs2d.AddByCenterStartEnd(0, 0, coordinates[6 * j + 2 + 3][2], coordinates[6 * j + 2 + 3][3], coordinates[6 * j + 2 + 3][0], coordinates[6 * j + 2 + 3][1]);
      }
      CreatingRelations2(relations2d, relation2d, profile);
      CloseProfile(profile);
      aProfiles = CreatingTheProfile(aProfiles, profile);
      models = ModellReferenc(models, part);
      profile.Visible = false;
      Extrude(kihuzas);
    }
    public void CreatingRelations2(SolidEdgeFrameworkSupport.Relations2d relations2dToCreat, SolidEdgeFrameworkSupport.Relation2d relation2dToCreat, SolidEdgePart.Profile profileForRelations)
    {
      relations2dToCreat = (SolidEdgeFrameworkSupport.Relations2d)
        profileForRelations.Relations2d;
      for (int i = 0; i < (24); i++)
      {
        relation2dToCreat = relations2dToCreat.AddKeypoint(
      arcs2d.Item(2 * i + 2),
      (int)KeypointIndexConstants.igArcEnd,
            bSplineCurves2d.Item(2 * i + 1),
      (int)SolidEdgeConstants.KeyPointType.igKeyPointEnd,
      true);
        relation2dToCreat = relations2dToCreat.AddKeypoint(
            arcs2d.Item(2 * i + 1),
     (int)KeypointIndexConstants.igArcStart,
     bSplineCurves2d.Item(2 * i + 1),
     (int)KeypointIndexConstants.igBsplineCurveStart,
     true);
        relation2dToCreat = relations2dToCreat.AddKeypoint(
      arcs2d.Item(2 * i + 2),
     (int)KeypointIndexConstants.igArcStart,
            bSplineCurves2d.Item(2 * i + 2),
     (int)KeypointIndexConstants.igBsplineCurveStart,
     true);
        relation2dToCreat = relations2dToCreat.AddKeypoint(

      arcs2d.Item(2 * i + 3),
      (int)KeypointIndexConstants.igArcEnd,
            bSplineCurves2d.Item(2 * i + 2),
      (int)SolidEdgeConstants.KeyPointType.igKeyPointEnd,
      true);

      }
      relation2dToCreat = relations2dToCreat.AddKeypoint(
       arcs2d.Item(arcs2d.Count),
       (int)KeypointIndexConstants.igArcStart,
      arcs2d.Item(1),
      (int)KeypointIndexConstants.igArcEnd,
       true);
    }
    public void CreatingRelations3(SolidEdgeFrameworkSupport.Relations2d relations2dToCreat, SolidEdgeFrameworkSupport.Relation2d relation2dToCreat, SolidEdgePart.Profile profileForRelations)
    {
      relations2dToCreat = (SolidEdgeFrameworkSupport.Relations2d)
        profileForRelations.Relations2d;
      relation2dToCreat = relations2dToCreat.AddKeypoint(
   lines2d.Item(1),
   (int)KeypointIndexConstants.igLineStart,
   arcs2d.Item(2),
   (int)KeypointIndexConstants.igArcStart,
   true);
      relation2dToCreat = relations2dToCreat.AddKeypoint(
       lines2d.Item(2),
       (int)KeypointIndexConstants.igLineEnd,
       arcs2d.Item(2),
       (int)KeypointIndexConstants.igArcEnd,
       true);
      relation2dToCreat = relations2dToCreat.AddKeypoint(
       lines2d.Item(2),
       (int)KeypointIndexConstants.igLineStart,
       arcs2d.Item(1),
       (int)KeypointIndexConstants.igArcStart,
       true);
      relation2dToCreat = relations2dToCreat.AddKeypoint(
       lines2d.Item(1),
       (int)KeypointIndexConstants.igLineEnd,
       arcs2d.Item(1),
       (int)KeypointIndexConstants.igArcEnd,
       true);

    }
  }
}