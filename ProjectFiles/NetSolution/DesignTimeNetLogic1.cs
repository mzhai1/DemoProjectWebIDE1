#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.WebUI;
using FTOptix.EventLogger;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.Store;
using FTOptix.Alarm;
using FTOptix.Core;
using FTOptix.RAEtherNetIP;
using FTOptix.CommunicationDriver;
using FTOptix.ODBCStore;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.OPCUAClient;
using FTOptix.Recipe;
using FTOptix.OPCUAServer;
#endregion

public class DesignTimeNetLogic1 : BaseNetLogic
{
    [ExportMethod]
    public void Method1()
    {
        // Insert code to be executed by the method
        var modelFolder = Project.Current.Get("Model");
        // Create PumpType folder under Model folder
        var pumpTypeFolder = InformationModel.MakeObject<Folder>("PumpType");
        modelFolder.Add(pumpTypeFolder);
        // Create WaterPump type under PumpType folder
        var waterPumpType = InformationModel.MakeObjectType("WaterPump");
        pumpTypeFolder.Add(waterPumpType);
        // Create variables for WaterPump type
        var enabledVariable = InformationModel.MakeVariable("Enabled", OpcUa.DataTypes.Boolean);
        var gphVariable = InformationModel.MakeVariable("GPH", OpcUa.DataTypes.Int32);
        var modelVariable = InformationModel.MakeVariable("Model", OpcUa.DataTypes.String);
        modelVariable.Value = "RX000";
        waterPumpType.Add(enabledVariable);
        waterPumpType.Add(gphVariable);
        waterPumpType.Add(modelVariable);
        // Create PumpInstance folder under Model folder
        var pumpInstanceFolder = InformationModel.MakeObject<Folder>("PumpInstance");
        modelFolder.Add(pumpInstanceFolder);
        // Create 3 instances of WaterPump type under PumpInstance folder
        var waterPumpInstance1 = InformationModel.MakeObject("WaterPump1", waterPumpType.NodeId);
        var waterPumpInstance2 = InformationModel.MakeObject("WaterPump2", waterPumpType.NodeId);
        var waterPumpInstance3 = InformationModel.MakeObject("WaterPump3", waterPumpType.NodeId);
        pumpInstanceFolder.Add(waterPumpInstance1);
        pumpInstanceFolder.Add(waterPumpInstance2);
        pumpInstanceFolder.Add(waterPumpInstance3);
        // Set True/10/RX010 to Enabled/GPH/Model for WaterPump1
        waterPumpInstance1.GetVariable("Enabled").Value = true;
        waterPumpInstance1.GetVariable("GPH").Value = 10;
        waterPumpInstance1.GetVariable("Model").Value = "RX010";
        // Set False/20/RX020 to Enabled/GPH/Model for WaterPump2
        waterPumpInstance2.GetVariable("Enabled").Value = false;
        waterPumpInstance2.GetVariable("GPH").Value = 20;
        waterPumpInstance2.GetVariable("Model").Value = "RX020";
        // Set True/30/RX030 to Enabled/GPH/Model for WaterPump3
        waterPumpInstance3.GetVariable("Enabled").Value = true;
        waterPumpInstance3.GetVariable("GPH").Value = 30;
        waterPumpInstance3.GetVariable("Model").Value = "RX030";
        //create a node pointer under Model folder
        var varWaterPumpSelectionNodePointer = InformationModel.MakeNodePointer("varWaterPumpSelection");
        modelFolder.Add(varWaterPumpSelectionNodePointer);
        // Create a combo box under MainWindow
        var netLogicPanel = Project.Current.Get("UI").Get("Panels").Get("NetLogic");
        var comboBox1 = InformationModel.MakeObject<ComboBox>("ComboBox1");
        netLogicPanel.Add(comboBox1);
        comboBox1.Width = 100;
        comboBox1.LeftMargin = 30;
        comboBox1.TopMargin = 5;
        comboBox1.Model = pumpInstanceFolder.NodeId;
        comboBox1.SelectedItemVariable.SetDynamicLink(varWaterPumpSelectionNodePointer, DynamicLinkMode.ReadWrite);
        comboBox1.SelectedItemVariable.GetVariable("DynamicLink").Value = comboBox1.SelectedItemVariable.GetVariable("DynamicLink").Value + "@Pointer";

        // Create a rectangle under MainWindow
        var rectangle1 = InformationModel.MakeObject<Rectangle>("Rectangle1");
        netLogicPanel.Add(rectangle1);
        rectangle1.Width = 150;
        rectangle1.Height = 150;
        rectangle1.LeftMargin = 30;
        rectangle1.TopMargin = 110;
        rectangle1.BorderThickness = 1;
        // Create a label under rectangle
        var label1 = InformationModel.MakeObject<Label>("label");
        label1.LeftMargin = 10;
        label1.TopMargin = 10;
        label1.TextVariable.SetDynamicLink(null);
        label1.TextVariable.GetVariable("DynamicLink").Value = "../../../ComboBox1/SelectedItem/Model";
        rectangle1.Add(label1);
        // Create a spin box under rectangle
        var spinBox1 = InformationModel.MakeObject<SpinBox>("SpinBox1");
        spinBox1.LeftMargin = 10;
        spinBox1.TopMargin = 50;
        spinBox1.ValueVariable.SetDynamicLink(null, DynamicLinkMode.ReadWrite);
        spinBox1.ValueVariable.GetVariable("DynamicLink").Value = "../../../ComboBox1/SelectedItem/GPH";
        rectangle1.Add(spinBox1);
        // Create a check box under rectangle
        var checkBox1 = InformationModel.MakeObject<CheckBox>("CheckBox1");
        checkBox1.CheckedVariable.SetDynamicLink(null, DynamicLinkMode.ReadWrite);
        checkBox1.CheckedVariable.GetVariable("DynamicLink").Value = "../../../ComboBox1/SelectedItem/Enabled";
        checkBox1.TextVariable.Value = "Enabled";
        checkBox1.LeftMargin = 10;
        checkBox1.TopMargin = 100;
        rectangle1.Add(checkBox1);
        
        // Create a data grid under MainWindow
        var dataGrid1 = InformationModel.MakeObject<DataGrid>("DataGrid1");
        netLogicPanel.Add(dataGrid1);
        dataGrid1.LeftMargin = 30;
        dataGrid1.TopMargin = 290;
        dataGrid1.Width = 300;
        dataGrid1.Height = 100;
        dataGrid1.Model = pumpInstanceFolder.NodeId;
        dataGrid1.SelectedItemVariable.SetDynamicLink(varWaterPumpSelectionNodePointer, DynamicLinkMode.ReadWrite);
        dataGrid1.SelectedItemVariable.GetVariable("DynamicLink").Value = dataGrid1.SelectedItemVariable.GetVariable("DynamicLink").Value + "@Pointer";
        // Add 3 EditableText Columns to DataGrid1
        var dataGridColumn1 = InformationModel.MakeObject<DataGridColumn>("DataGridColumn1");
        var dataGridColumn2 = InformationModel.MakeObject<DataGridColumn>("DataGridColumn2");
        var dataGridColumn3 = InformationModel.MakeObject<DataGridColumn>("DataGridColumn3");
        var dataItemTemplate1 = InformationModel.MakeObject<DataGridEditableLabelItemTemplate>("DataItemTemplate");
        var dataItemTemplate2 = InformationModel.MakeObject<DataGridEditableLabelItemTemplate>("DataItemTemplate");
        var dataItemTemplate3 = InformationModel.MakeObject<DataGridEditableLabelItemTemplate>("DataItemTemplate");
        dataGridColumn1.DataItemTemplate = dataItemTemplate1;
        dataGridColumn2.DataItemTemplate = dataItemTemplate2;
        dataGridColumn3.DataItemTemplate = dataItemTemplate3;
        dataGrid1.Columns.Add(dataGridColumn1);
        dataGrid1.Columns.Add(dataGridColumn2);
        dataGrid1.Columns.Add(dataGridColumn3);
        dataItemTemplate1.GetVariable("Text").SetDynamicLink(null, DynamicLinkMode.ReadWrite);
        dataItemTemplate1.GetVariable("Text").GetVariable("DynamicLink").Value = "{Item}@BrowseName";
        dataGridColumn1.Title = "Name";
        dataItemTemplate2.GetVariable("Text").SetDynamicLink(null, DynamicLinkMode.ReadWrite);
        dataItemTemplate2.GetVariable("Text").GetVariable("DynamicLink").Value = "{Item}/Enabled";
        dataGridColumn2.Title = "Enabled";
        dataItemTemplate3.GetVariable("Text").SetDynamicLink(null, DynamicLinkMode.ReadWrite);
        dataItemTemplate3.GetVariable("Text").GetVariable("DynamicLink").Value = "{Item}/GPH";
        dataGridColumn3.Title = "GPH";

    }
}
