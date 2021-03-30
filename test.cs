// Decompiled with JetBrains decompiler
// Type: RTD_BT.Main
// Assembly: RTD-BT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 055DEAAA-1E54-4A30-A753-F99003D7379F
// Assembly location: C:\Users\Administrator\Downloads\새 폴더 (2)\새 폴더 (2)\RTD-BT.exe

using Bluegiga;
using Bluegiga.BLE.Events.ATTClient;
using Bluegiga.BLE.Events.Connection;
using Bluegiga.BLE.Events.GAP;
using Bluegiga.BLE.Events.System;
using C1.Win.C1Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RTD_BT
{
  public class Main : Form
  {
    private List<Panel> Panel_RT_Check = new List<Panel>();
    private List<Panel> Panel_RT_UserName = new List<Panel>();
    private List<Panel> Panel_RT_Value = new List<Panel>();
    private List<PictureBox> Picture_RT_Check = new List<PictureBox>();
    private List<Label> Label_RT_UserName = new List<Label>();
    private List<Label> Label_RT_Value = new List<Label>();
    private List<Panel> Panel_Config_Name = new List<Panel>();
    private List<Panel> Panel_Config_Day = new List<Panel>();
    private List<Panel> Panel_Config_Month = new List<Panel>();
    private List<Panel> Panel_Config_Year = new List<Panel>();
    private List<Panel> Panel_Config_Address = new List<Panel>();
    private List<Panel> Panel_Config_Port = new List<Panel>();
    private List<Label> Label_Config_Name = new List<Label>();
    private List<Label> Label_Config_Day = new List<Label>();
    private List<Label> Label_Config_Month = new List<Label>();
    private List<Label> Label_Config_Year = new List<Label>();
    private List<Label> Label_Config_Address = new List<Label>();
    private List<Label> Label_Config_Port = new List<Label>();
    private List<PictureBox> Picture_Timer_Set = new List<PictureBox>();
    private Color DefaultColor = Color.FromArgb(224, 224, 224);
    private Color AlertNormal = Color.FromArgb(120, (int) byte.MaxValue, 120);
    private Color AlertWarning = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 120);
    private Color AlertCritical = Color.FromArgb((int) byte.MaxValue, 120, 120);
    private Color AlertDoseNormal = Color.FromArgb(192, (int) byte.MaxValue, 192);
    private Color AlertDoseWarning = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
    private Color AlertDoseCritical = Color.FromArgb((int) byte.MaxValue, 192, 192);
    private const int USER_SIZE = 6;
    private readonly string diskPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private UserInfo[] user;
    private List<string>[] listTimeCPS = new List<string>[6];
    private const int LIST_SIZE = 300;
    private int[] logWriteCount = new int[6];
    private int[] warning = (int[]) null;
    private int[] critical = (int[]) null;
    public static System.Windows.Forms.Timer[] tmCPSData = new System.Windows.Forms.Timer[6];
    private System.Windows.Forms.Timer[] tmBTActive = new System.Windows.Forms.Timer[6];
    private System.Windows.Forms.Timer tmUpdateGraph = new System.Windows.Forms.Timer();
    private SerialPort[] serialPort = new SerialPort[6];
    private int[] BTActiveDataCount = new int[6];
    private int[] BTActiveState = new int[6];
    private const int BT_ACTIVE_TOLERANCE = 6;
    private bool[] BTLock = new bool[6];
    private List<string>[] BluetoothTarget = new List<string>[6];
    private List<byte[]>[] BluetoothTarget_Address = new List<byte[]>[6];
    public BGLib[] bglib = new BGLib[6];
    public GroupFoundEventArgs[] grpEvt = new GroupFoundEventArgs[6];
    private List<FindInformationFoundEventArgs>[] handleTable = new List<FindInformationFoundEventArgs>[6];
    private Main.BLE_STATE[] BLE_State = new Main.BLE_STATE[6];
    private Form_EXIT formExit = new Form_EXIT();
    private Form_Progress formProgress = new Form_Progress();
    private Form_TimerSaver formTimerSaver = new Form_TimerSaver();
    private Thread exitWatch = (Thread) null;
    private Thread deviceThread = (Thread) null;
    private List<byte[]> BTtest = new List<byte[]>();
    private byte[] BTtest2;
    private IContainer components = (IContainer) null;
    private TableLayoutPanel tableLayoutPanel_Main;
    private TableLayoutPanel tableLayoutPanel_TopMenu;
    private PictureBox pictureBox_CompanyLogo;
    private PictureBox pictureBox_Exit;
    private C1DockingTab c1DockingTab_Main;
    private C1DockingTabPage c1DockingTabPage_RT;
    private C1DockingTabPage c1DockingTabPage_Settings;
    private TableLayoutPanel tableLayoutPanel_RT_Main;
    private Chart chart_RT_CPSChart;
    private TableLayoutPanel tableLayoutPanel_RT_Head1;
    private TableLayoutPanel tableLayoutPanel_RT_TLD1;
    private TableLayoutPanel tableLayoutPanel_RT_TLD3;
    private TableLayoutPanel tableLayoutPanel_RT_TLD3_Check;
    private TableLayoutPanel tableLayoutPanel_RT_TLD1_Check;
    private TableLayoutPanel tableLayoutPanel_RT_TLD2;
    private TableLayoutPanel tableLayoutPanel_RT_TLD2_Check;
    private PictureBox pictureBox_RT_TLD3_Check;
    private PictureBox pictureBox_RT_TLD1_Check;
    private PictureBox pictureBox_RT_TLD2_Check;
    private Panel panel_RT_TLD3_UserName;
    private Panel panel_RT_TLD2_UserName;
    private Panel panel_RT_TLD3_Value;
    private Panel panel_RT_TLD1_Value;
    private Panel panel_RT_TLD2_Value;
    private PictureBox pictureBox_ProductName;
    private Panel panel_Config_User1_day;
    private Panel panel_Config_User1_month;
    private Panel panel_Config_User1_year;
    private Panel panel_Config_User1_BTAddr;
    private Panel panel_Config_User1_COM;
    private Panel panel_Config_User3_Name;
    private Panel panel_Config_User2_Name;
    private Panel panel_Config_User3_day;
    private Panel panel_Config_User3_COM;
    private Panel panel_Config_User3_BTAddr;
    private Panel panel_Config_User3_year;
    private Panel panel_Config_User2_day;
    private Panel panel_Config_User2_month;
    private Panel panel_Config_User2_BTAddr;
    private Panel panel_Config_User2_COM;
    private Panel panel_Config_User2_year;
    private Panel panel_RT_TLD1_Check;
    private Panel panel_RT_TLD3_Check;
    private Panel panel_RT_TLD2_Check;
    private Label label_Config_Title_Name;
    private PictureBox pictureBox_user3_timer;
    private PictureBox pictureBox_user2_timer;
    private PictureBox pictureBox_user1_timer;
    private TableLayoutPanel tableLayoutPanel_RT_Head2;
    private TableLayoutPanel tableLayoutPanel_RT_TLD6;
    private TableLayoutPanel tableLayoutPanel_RT_TLD6_Check;
    private Panel panel_RT_TLD6_Check;
    private PictureBox pictureBox_RT_TLD6_Check;
    private Panel panel_RT_TLD6_UserName;
    private Panel panel_RT_TLD6_Value;
    private TableLayoutPanel tableLayoutPanel_RT_TLD4;
    private TableLayoutPanel tableLayoutPanel_RT_TLD4_Check;
    private Panel panel_RT_TLD4_Check;
    private PictureBox pictureBox_RT_TLD4_Check;
    private Panel panel_RT_TLD4_UserName;
    private Panel panel_RT_TLD4_Value;
    private TableLayoutPanel tableLayoutPanel_RT_TLD5;
    private TableLayoutPanel tableLayoutPanel_RT_TLD5_Check;
    private Panel panel_RT_TLD5_UserName;
    private Panel panel_RT_TLD5_Check;
    private PictureBox pictureBox_RT_TLD5_Check;
    private Panel panel_RT_TLD5_Value;
    private Panel panel_RT_Chart;
    private PictureBox pictureBox_RT_Chart_Time;
    private PictureBox pictureBox_RT_Chart_uSv;
    private PictureBox pictureBox_RT_Chart_CPS;
    private Panel panel_RT_TLD1_UserName;
    private TableLayoutPanel tableLayoutPanel_Config_Main;
    private TableLayoutPanel tableLayoutPanel_Config_UserTitle;
    private TableLayoutPanel tableLayoutPanel_Config_WarningCriticalTitle;
    private TableLayoutPanel tableLayoutPanel_Config_Under;
    private Label label_Config_Warning_Name;
    private Panel panel_Config_Warning_Name;
    private Label label_RT_TLD6_UserName;
    private Label label_RT_TLD6_Value;
    private Label label_RT_TLD4_UserName;
    private Label label_RT_TLD4_Value;
    private Label label_RT_TLD5_UserName;
    private Label label_RT_TLD5_Value;
    private Label label_RT_TLD3_UserName;
    private Label label_RT_TLD3_Value;
    private Label label_RT_TLD1_UserName;
    private Label label_RT_TLD1_Value;
    private Label label_RT_TLD2_UserName;
    private Label label_RT_TLD2_Value;
    private Panel panel_Config_criticalDay;
    private Label label_Config_criticalDay;
    private Panel panel_Config_Critical_Name;
    private Label label_Config_WarningCriticalTitle_Day;
    private Label label_Config_warningDay;
    private Label label_Config_User3_COM;
    private Label label_Config_User3_Name;
    private Label label_Config_User3_BTAddr;
    private Label label_Config_User3_year;
    private Panel panel_Config_User3_month;
    private Label label_Config_User3_month;
    private Label label_Config_User2_COM;
    private Label label_Config_User3_day;
    private Label label_Config_User2_BTAddr;
    private Label label_Config_User2_Name;
    private Label label_Config_User1_COM;
    private Label label_Config_User2_year;
    private Panel panel_Config_User1_Name;
    private Label label_Config_User1_Name;
    private Label label_Config_User2_month;
    private Label label_Config_User1_BTAddr;
    private Label label_Config_User2_day;
    private Label label_Config_User1_year;
    private Label label_Config_User1_month;
    private Label label_Config_User1_day;
    private Label label_Config_Title_day;
    private Label label_Config_Title_month;
    private Label label_Config_Title_year;
    private Label label_Config_Title_BTAddr;
    private Label label_Config_Title_COM;
    private Panel panel_Config_criticalMonth;
    private Label label_Config_criticalMonth;
    private Panel panel_Config_warningMonth;
    private Label label_Config_warningMonth;
    private Label label_Config_WarningCriticalTitle_Month;
    private Label label_Config_WarningCriticalTitle_Year;
    private Panel panel_Config_warningYear;
    private Label label_Config_warningYear;
    private Panel panel_Config_criticalYear;
    private Label label_Config_criticalYear;
    private Label label_Config_WarningCriticalTitle_Tick;
    private Panel panel_Config_warningTick;
    private Label label_Config_warningTick;
    private Panel panel_Config_criticalTick;
    private Label label_Config_criticalTick;
    private Panel panel_Config_warningDay;
    private Label label_Config_Critical_Name;
    private Panel panel_Config_User4_Name;
    private Label label_Config_User4_Name;
    private Panel panel_Config_User4_day;
    private Label label_Config_User4_day;
    private Panel panel_Config_User4_month;
    private Label label_Config_User4_month;
    private Panel panel_Config_User4_year;
    private Label label_Config_User4_year;
    private Panel panel_Config_User4_BTAddr;
    private Label label_Config_User4_BTAddr;
    private Panel panel_Config_User4_COM;
    private Label label_Config_User4_COM;
    private Panel panel_Config_User5_Name;
    private Label label_Config_User5_Name;
    private Panel panel_Config_User5_day;
    private Label label_Config_User5_day;
    private Panel panel_Config_User5_month;
    private Label label_Config_User5_month;
    private Panel panel_Config_User5_year;
    private Label label_Config_User5_year;
    private Panel panel_Config_User5_BTAddr;
    private Label label_Config_User5_BTAddr;
    private Panel panel_Config_User5_COM;
    private Label label_Config_User5_COM;
    private Panel panel_Config_User6_Name;
    private Label label_Config_User6_Name;
    private Panel panel_Config_User6_day;
    private Label label_Config_User6_day;
    private Panel panel_Config_User6_month;
    private Label label_Config_User6_month;
    private Panel panel_Config_User6_year;
    private Label label_Config_User6_year;
    private Panel panel_Config_User6_BTAddr;
    private Label label_Config_User6_BTAddr;
    private Panel panel_Config_User6_COM;
    private Label label_Config_User6_COM;
    private PictureBox pictureBox_user6_timer;
    private PictureBox pictureBox_user4_timer;
    private PictureBox pictureBox_user5_timer;
    private TableLayoutPanel tableLayoutPanel1;
    private Label label1;

    public void Init()
    {
      try
      {
        for (int index1 = 0; index1 < 6; ++index1)
        {
          this.listTimeCPS[index1] = new List<string>();
          for (int index2 = 0; index2 < 300; ++index2)
            this.listTimeCPS[index1].Add("0.0.0. -.0/0");
        }
        this.InitUI();
        this.InitCommunication();
        this.LoadConfig();
        this.SetInfo();
        this.SetTimer();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("오류 발생 : Error Code (00)" + ex.ToString());
        Application.ExitThread();
        Environment.Exit(0);
      }
    }

    public void InitUI()
    {
      this.UpdateUIThread((MethodInvoker) (() =>
      {
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Size = new Size(1024, 600);
        this.SetUIObjects();
        this.tableLayoutPanel_Main.Dock = DockStyle.Fill;
        this.tableLayoutPanel_TopMenu.Dock = DockStyle.Fill;
        this.pictureBox_CompanyLogo.Dock = DockStyle.Fill;
        this.pictureBox_ProductName.Dock = DockStyle.Fill;
        this.pictureBox_Exit.Dock = DockStyle.Fill;
        this.c1DockingTab_Main.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_Main.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_Head1.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_Head2.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD1.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD2.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD3.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD4.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD5.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD6.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD1_Check.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD2_Check.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD3_Check.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD4_Check.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD5_Check.Dock = DockStyle.Fill;
        this.tableLayoutPanel_RT_TLD6_Check.Dock = DockStyle.Fill;
        for (int index = 0; index < 6; ++index)
        {
          this.Panel_RT_Check[index].Dock = DockStyle.Fill;
          this.Panel_RT_UserName[index].Dock = DockStyle.Fill;
          this.Panel_RT_Value[index].Dock = DockStyle.Fill;
          this.Picture_RT_Check[index].Dock = DockStyle.Fill;
          this.Label_RT_UserName[index].Dock = DockStyle.Fill;
          this.Label_RT_Value[index].BackColor = Color.Transparent;
          this.Label_RT_Value[index].Dock = DockStyle.Fill;
          this.Label_RT_Value[index].Text = "-";
        }
        this.panel_RT_Chart.Dock = DockStyle.Fill;
        this.chart_RT_CPSChart.Dock = DockStyle.Fill;
        this.InitChartCPSStyle(this.chart_RT_CPSChart);
        this.Main_Resize((object) null, (EventArgs) null);
        this.tableLayoutPanel_Config_Main.Dock = DockStyle.Fill;
        this.tableLayoutPanel_Config_UserTitle.Dock = DockStyle.Fill;
        this.tableLayoutPanel_Config_WarningCriticalTitle.Dock = DockStyle.Fill;
        this.tableLayoutPanel_Config_Under.Dock = DockStyle.Fill;
        this.tableLayoutPanel1.Dock = DockStyle.Fill;
        this.label_Config_Title_Name.Dock = DockStyle.Fill;
        this.label_Config_Title_day.Dock = DockStyle.Fill;
        this.label_Config_Title_month.Dock = DockStyle.Fill;
        this.label_Config_Title_year.Dock = DockStyle.Fill;
        this.label_Config_Title_BTAddr.Dock = DockStyle.Fill;
        this.label_Config_Title_COM.Dock = DockStyle.Fill;
        this.label1.Dock = DockStyle.Fill;
        this.label_Config_Title_Name.Dock = DockStyle.Fill;
        for (int index = 0; index < 6; ++index)
        {
          this.Panel_Config_Name[index].Dock = DockStyle.Fill;
          this.Panel_Config_Day[index].Dock = DockStyle.Fill;
          this.Panel_Config_Month[index].Dock = DockStyle.Fill;
          this.Panel_Config_Year[index].Dock = DockStyle.Fill;
          this.Panel_Config_Address[index].Dock = DockStyle.Fill;
          this.Panel_Config_Port[index].Dock = DockStyle.Fill;
          this.Label_Config_Name[index].Dock = DockStyle.Fill;
          this.Label_Config_Day[index].Dock = DockStyle.Fill;
          this.Label_Config_Month[index].Dock = DockStyle.Fill;
          this.Label_Config_Year[index].Dock = DockStyle.Fill;
          this.Label_Config_Address[index].Dock = DockStyle.Fill;
          this.Label_Config_Address[index].Text = "";
          this.Label_Config_Port[index].Dock = DockStyle.Fill;
          this.Label_Config_Port[index].Text = "";
          this.Picture_Timer_Set[index].Dock = DockStyle.Fill;
        }
        this.panel_Config_Warning_Name.Dock = DockStyle.Fill;
        this.panel_Config_Critical_Name.Dock = DockStyle.Fill;
        this.panel_Config_warningDay.Dock = DockStyle.Fill;
        this.panel_Config_criticalDay.Dock = DockStyle.Fill;
        this.panel_Config_warningMonth.Dock = DockStyle.Fill;
        this.panel_Config_criticalMonth.Dock = DockStyle.Fill;
        this.panel_Config_warningYear.Dock = DockStyle.Fill;
        this.panel_Config_criticalYear.Dock = DockStyle.Fill;
        this.panel_Config_warningTick.Dock = DockStyle.Fill;
        this.panel_Config_criticalTick.Dock = DockStyle.Fill;
        this.label_Config_Warning_Name.Dock = DockStyle.Fill;
        this.label_Config_Critical_Name.Dock = DockStyle.Fill;
        this.label_Config_WarningCriticalTitle_Day.Dock = DockStyle.Fill;
        this.label_Config_warningDay.Dock = DockStyle.Fill;
        this.label_Config_criticalDay.Dock = DockStyle.Fill;
        this.label_Config_WarningCriticalTitle_Month.Dock = DockStyle.Fill;
        this.label_Config_warningMonth.Dock = DockStyle.Fill;
        this.label_Config_criticalMonth.Dock = DockStyle.Fill;
        this.label_Config_WarningCriticalTitle_Year.Dock = DockStyle.Fill;
        this.label_Config_warningYear.Dock = DockStyle.Fill;
        this.label_Config_criticalYear.Dock = DockStyle.Fill;
        this.label_Config_WarningCriticalTitle_Tick.Dock = DockStyle.Fill;
        this.label_Config_warningTick.Dock = DockStyle.Fill;
        this.label_Config_criticalTick.Dock = DockStyle.Fill;
      }));
      for (int index = 0; index < 6; ++index)
      {
        int EventIndex = index;
        this.Panel_RT_Check[index].DoubleClick += (EventHandler) ((s, e) => this.RT_Check_DoubleClick(EventIndex));
        this.Picture_RT_Check[index].DoubleClick += (EventHandler) ((s, e) => this.RT_Check_DoubleClick(EventIndex));
        this.Picture_Timer_Set[index].Click += (EventHandler) ((s, e) => this.User_Log_Interval(EventIndex));
      }
    }

    public void SetUIObjects() => this.UpdateUIThread((MethodInvoker) (() =>
    {
      this.Panel_RT_Check.Add(this.panel_RT_TLD1_Check);
      this.Picture_RT_Check.Add(this.pictureBox_RT_TLD1_Check);
      this.Panel_RT_UserName.Add(this.panel_RT_TLD1_UserName);
      this.Label_RT_UserName.Add(this.label_RT_TLD1_UserName);
      this.Panel_RT_Value.Add(this.panel_RT_TLD1_Value);
      this.Label_RT_Value.Add(this.label_RT_TLD1_Value);
      this.Panel_Config_Name.Add(this.panel_Config_User1_Name);
      this.Label_Config_Name.Add(this.label_Config_User1_Name);
      this.Panel_Config_Day.Add(this.panel_Config_User1_day);
      this.Label_Config_Day.Add(this.label_Config_User1_day);
      this.Panel_Config_Month.Add(this.panel_Config_User1_month);
      this.Label_Config_Month.Add(this.label_Config_User1_month);
      this.Panel_Config_Year.Add(this.panel_Config_User1_year);
      this.Label_Config_Year.Add(this.label_Config_User1_year);
      this.Panel_Config_Address.Add(this.panel_Config_User1_BTAddr);
      this.Label_Config_Address.Add(this.label_Config_User1_BTAddr);
      this.Panel_Config_Port.Add(this.panel_Config_User1_COM);
      this.Label_Config_Port.Add(this.label_Config_User1_COM);
      this.Picture_Timer_Set.Add(this.pictureBox_user1_timer);
      this.Panel_RT_Check.Add(this.panel_RT_TLD2_Check);
      this.Picture_RT_Check.Add(this.pictureBox_RT_TLD2_Check);
      this.Panel_RT_UserName.Add(this.panel_RT_TLD2_UserName);
      this.Label_RT_UserName.Add(this.label_RT_TLD2_UserName);
      this.Panel_RT_Value.Add(this.panel_RT_TLD2_Value);
      this.Label_RT_Value.Add(this.label_RT_TLD2_Value);
      this.Panel_Config_Name.Add(this.panel_Config_User2_Name);
      this.Label_Config_Name.Add(this.label_Config_User2_Name);
      this.Panel_Config_Day.Add(this.panel_Config_User2_day);
      this.Label_Config_Day.Add(this.label_Config_User2_day);
      this.Panel_Config_Month.Add(this.panel_Config_User2_month);
      this.Label_Config_Month.Add(this.label_Config_User2_month);
      this.Panel_Config_Year.Add(this.panel_Config_User2_year);
      this.Label_Config_Year.Add(this.label_Config_User2_year);
      this.Panel_Config_Address.Add(this.panel_Config_User2_BTAddr);
      this.Label_Config_Address.Add(this.label_Config_User2_BTAddr);
      this.Panel_Config_Port.Add(this.panel_Config_User2_COM);
      this.Label_Config_Port.Add(this.label_Config_User2_COM);
      this.Picture_Timer_Set.Add(this.pictureBox_user2_timer);
      this.Panel_RT_Check.Add(this.panel_RT_TLD3_Check);
      this.Picture_RT_Check.Add(this.pictureBox_RT_TLD3_Check);
      this.Panel_RT_UserName.Add(this.panel_RT_TLD3_UserName);
      this.Label_RT_UserName.Add(this.label_RT_TLD3_UserName);
      this.Panel_RT_Value.Add(this.panel_RT_TLD3_Value);
      this.Label_RT_Value.Add(this.label_RT_TLD3_Value);
      this.Panel_Config_Name.Add(this.panel_Config_User3_Name);
      this.Label_Config_Name.Add(this.label_Config_User3_Name);
      this.Panel_Config_Day.Add(this.panel_Config_User3_day);
      this.Label_Config_Day.Add(this.label_Config_User3_day);
      this.Panel_Config_Month.Add(this.panel_Config_User3_month);
      this.Label_Config_Month.Add(this.label_Config_User3_month);
      this.Panel_Config_Year.Add(this.panel_Config_User3_year);
      this.Label_Config_Year.Add(this.label_Config_User3_year);
      this.Panel_Config_Address.Add(this.panel_Config_User3_BTAddr);
      this.Label_Config_Address.Add(this.label_Config_User3_BTAddr);
      this.Panel_Config_Port.Add(this.panel_Config_User3_COM);
      this.Label_Config_Port.Add(this.label_Config_User3_COM);
      this.Picture_Timer_Set.Add(this.pictureBox_user3_timer);
      this.Panel_RT_Check.Add(this.panel_RT_TLD4_Check);
      this.Picture_RT_Check.Add(this.pictureBox_RT_TLD4_Check);
      this.Panel_RT_UserName.Add(this.panel_RT_TLD4_UserName);
      this.Label_RT_UserName.Add(this.label_RT_TLD4_UserName);
      this.Panel_RT_Value.Add(this.panel_RT_TLD4_Value);
      this.Label_RT_Value.Add(this.label_RT_TLD4_Value);
      this.Panel_Config_Name.Add(this.panel_Config_User4_Name);
      this.Label_Config_Name.Add(this.label_Config_User4_Name);
      this.Panel_Config_Day.Add(this.panel_Config_User4_day);
      this.Label_Config_Day.Add(this.label_Config_User4_day);
      this.Panel_Config_Month.Add(this.panel_Config_User4_month);
      this.Label_Config_Month.Add(this.label_Config_User4_month);
      this.Panel_Config_Year.Add(this.panel_Config_User4_year);
      this.Label_Config_Year.Add(this.label_Config_User4_year);
      this.Panel_Config_Address.Add(this.panel_Config_User4_BTAddr);
      this.Label_Config_Address.Add(this.label_Config_User4_BTAddr);
      this.Panel_Config_Port.Add(this.panel_Config_User4_COM);
      this.Label_Config_Port.Add(this.label_Config_User4_COM);
      this.Picture_Timer_Set.Add(this.pictureBox_user4_timer);
      this.Panel_RT_Check.Add(this.panel_RT_TLD5_Check);
      this.Picture_RT_Check.Add(this.pictureBox_RT_TLD5_Check);
      this.Panel_RT_UserName.Add(this.panel_RT_TLD5_UserName);
      this.Label_RT_UserName.Add(this.label_RT_TLD5_UserName);
      this.Panel_RT_Value.Add(this.panel_RT_TLD5_Value);
      this.Label_RT_Value.Add(this.label_RT_TLD5_Value);
      this.Panel_Config_Name.Add(this.panel_Config_User5_Name);
      this.Label_Config_Name.Add(this.label_Config_User5_Name);
      this.Panel_Config_Day.Add(this.panel_Config_User5_day);
      this.Label_Config_Day.Add(this.label_Config_User5_day);
      this.Panel_Config_Month.Add(this.panel_Config_User5_month);
      this.Label_Config_Month.Add(this.label_Config_User5_month);
      this.Panel_Config_Year.Add(this.panel_Config_User5_year);
      this.Label_Config_Year.Add(this.label_Config_User5_year);
      this.Panel_Config_Address.Add(this.panel_Config_User5_BTAddr);
      this.Label_Config_Address.Add(this.label_Config_User5_BTAddr);
      this.Panel_Config_Port.Add(this.panel_Config_User5_COM);
      this.Label_Config_Port.Add(this.label_Config_User5_COM);
      this.Picture_Timer_Set.Add(this.pictureBox_user5_timer);
      this.Panel_RT_Check.Add(this.panel_RT_TLD6_Check);
      this.Picture_RT_Check.Add(this.pictureBox_RT_TLD6_Check);
      this.Panel_RT_UserName.Add(this.panel_RT_TLD6_UserName);
      this.Label_RT_UserName.Add(this.label_RT_TLD6_UserName);
      this.Panel_RT_Value.Add(this.panel_RT_TLD6_Value);
      this.Label_RT_Value.Add(this.label_RT_TLD6_Value);
      this.Panel_Config_Name.Add(this.panel_Config_User6_Name);
      this.Label_Config_Name.Add(this.label_Config_User6_Name);
      this.Panel_Config_Day.Add(this.panel_Config_User6_day);
      this.Label_Config_Day.Add(this.label_Config_User6_day);
      this.Panel_Config_Month.Add(this.panel_Config_User6_month);
      this.Label_Config_Month.Add(this.label_Config_User6_month);
      this.Panel_Config_Year.Add(this.panel_Config_User6_year);
      this.Label_Config_Year.Add(this.label_Config_User6_year);
      this.Panel_Config_Address.Add(this.panel_Config_User6_BTAddr);
      this.Label_Config_Address.Add(this.label_Config_User6_BTAddr);
      this.Panel_Config_Port.Add(this.panel_Config_User6_COM);
      this.Label_Config_Port.Add(this.label_Config_User6_COM);
      this.Picture_Timer_Set.Add(this.pictureBox_user6_timer);
    }));

    private void InitCommunication()
    {
      for (int index = 0; index < 6; ++index)
      {
        this.BLE_State[index] = Main.BLE_STATE.STANDBY;
        this.BTLock[index] = false;
        this.BTActiveDataCount[index] = 0;
        this.BTActiveState[index] = 0;
        this.BluetoothTarget[index] = new List<string>();
        this.BluetoothTarget_Address[index] = new List<byte[]>();
      }
    }

    private void LoadConfig()
    {
      this.user = new UserInfo[6];
      for (int userIndex = 0; userIndex < 6; ++userIndex)
      {
        this.user[userIndex] = new UserInfo(userIndex);
        this.user[userIndex].TimeSet(0, 0, 10);
      }
      string path = "./config/config.txt";
      if (!File.Exists(path))
      {
        int num = (int) MessageBox.Show("프로그램이 손상되었습니다!\n관리자에게 문의하십시오.\n(jstw@daum.net, 053-000-000)\n\n프로그램을 종료합니다.", "No Config File", MessageBoxButtons.OK);
        Application.Exit();
      }
      else
      {
        bool flag = true;
        StreamReader streamReader = new StreamReader(path);
        while (!streamReader.EndOfStream)
        {
          if (streamReader.ReadLine().Contains("<Radiation Limit Config>"))
          {
            this.warning = new int[4];
            this.critical = new int[4];
            try
            {
              while (!streamReader.EndOfStream)
              {
                string str1 = streamReader.ReadLine();
                if (!str1.Contains("</Radiation Limit Config>"))
                {
                  string[] strArray = str1.Split('=');
                  string str2 = strArray[0];
                  if (!(str2 == "warning"))
                  {
                    if (str2 == "critical")
                    {
                      this.critical[0] = Convert.ToInt32(strArray[1].Split('/')[0]);
                      this.critical[1] = Convert.ToInt32(strArray[1].Split('/')[1]);
                      this.critical[2] = Convert.ToInt32(strArray[1].Split('/')[2]);
                      this.critical[3] = Convert.ToInt32(strArray[1].Split('/')[3]);
                    }
                    else
                      flag = false;
                  }
                  else
                  {
                    this.warning[0] = Convert.ToInt32(strArray[1].Split('/')[0]);
                    this.warning[1] = Convert.ToInt32(strArray[1].Split('/')[1]);
                    this.warning[2] = Convert.ToInt32(strArray[1].Split('/')[2]);
                    this.warning[3] = Convert.ToInt32(strArray[1].Split('/')[3]);
                  }
                }
                else
                  break;
              }
            }
            catch (Exception ex)
            {
              ex.ToString();
              flag = false;
            }
          }
        }
        streamReader.Close();
        if (!flag)
        {
          int num = (int) MessageBox.Show("프로그램이 손상되었습니다!\n관리자에게 문의하십시오.\n(jstw@daum.net, 053-000-000)\n\n프로그램을 종료합니다.", "Config Value Error", MessageBoxButtons.OK);
          Environment.Exit(0);
          Application.ExitThread();
        }
      }
    }

    private void SetInfo() => this.UpdateUIThread((MethodInvoker) (() =>
    {
      for (int index = 0; index < 6; ++index)
      {
        this.Label_Config_Address[index].Text = this.user[index].BTAddr;
        this.Label_Config_Port[index].Text = this.user[index].COMPort;
        this.UpdateDoseValue(index);
        this.UpdateUserInfoInterface(index, this.user[index].isUsing, this.user[index].UserName);
      }
      this.label_Config_warningDay.Text = string.Format("{0}", (object) this.warning[0]);
      this.label_Config_warningMonth.Text = string.Format("{0}", (object) this.warning[1]);
      this.label_Config_warningYear.Text = string.Format("{0}", (object) this.warning[2]);
      this.label_Config_warningTick.Text = string.Format("{0}", (object) this.warning[3]);
      this.label_Config_criticalDay.Text = string.Format("{0}", (object) this.critical[0]);
      this.label_Config_criticalMonth.Text = string.Format("{0}", (object) this.critical[1]);
      this.label_Config_criticalYear.Text = string.Format("{0}", (object) this.critical[2]);
      this.label_Config_criticalTick.Text = string.Format("{0}", (object) this.critical[3]);
    }));

    private void UpdateUserInfoInterface(int index, bool check, string name) => this.UpdateUIThread((MethodInvoker) (() =>
    {
      this.Picture_RT_Check[index].Visible = check;
      this.Label_RT_UserName[index].Visible = check;
      this.Label_RT_Value[index].Visible = check;
      this.Label_RT_UserName[index].Text = name;
      this.Label_Config_Name[index].Text = name;
    }));

    private void UpdateAlert()
    {
      for (int i = 0; i < 6; i++)
      {
        if (this.user[i].isUsing)
        {
          switch (this.CheckCPS(i))
          {
            case 0:
              this.UpdateUIThread((MethodInvoker) (() =>
              {
                this.Label_RT_Value[i].BackColor = this.AlertNormal;
                this.Label_Config_Name[i].BackColor = this.AlertNormal;
              }));
              break;
            case 1:
              this.UpdateUIThread((MethodInvoker) (() =>
              {
                this.Label_RT_Value[i].BackColor = this.AlertWarning;
                this.Label_Config_Name[i].BackColor = this.AlertWarning;
              }));
              break;
            case 2:
              this.UpdateUIThread((MethodInvoker) (() =>
              {
                this.Label_RT_Value[i].BackColor = this.AlertCritical;
                this.Label_Config_Name[i].BackColor = this.AlertCritical;
              }));
              break;
          }
        }
        else
          this.UpdateUIThread((MethodInvoker) (() =>
          {
            this.Label_RT_Value[i].BackColor = this.DefaultColor;
            this.Label_Config_Name[i].BackColor = this.DefaultColor;
          }));
        switch (this.CheckDose(i, 0))
        {
          case 0:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Day[i].BackColor = this.AlertNormal));
            break;
          case 1:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Day[i].BackColor = this.AlertWarning));
            break;
          case 2:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Day[i].BackColor = this.AlertCritical));
            break;
        }
        switch (this.CheckDose(i, 1))
        {
          case 0:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Month[i].BackColor = this.AlertNormal));
            break;
          case 1:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Month[i].BackColor = this.AlertWarning));
            break;
          case 2:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Month[i].BackColor = this.AlertCritical));
            break;
        }
        switch (this.CheckDose(i, 2))
        {
          case 0:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Year[i].BackColor = this.AlertNormal));
            break;
          case 1:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Year[i].BackColor = this.AlertWarning));
            break;
          case 2:
            this.UpdateUIThread((MethodInvoker) (() => this.Label_Config_Year[i].BackColor = this.AlertCritical));
            break;
        }
      }
    }

    private void UpdateDoseValue(int index) => this.UpdateUIThread((MethodInvoker) (() =>
    {
      this.Label_Config_Day[index].Text = string.Format("{0:f2}", (object) this.user[index].Dose_Day);
      this.Label_Config_Month[index].Text = string.Format("{0:f2}", (object) this.user[index].Dose_Month);
      this.Label_Config_Year[index].Text = string.Format("{0:f2}", (object) this.user[index].Dose_Year);
    }));

    private void Main_Resize(object sender, EventArgs e)
    {
      int num1 = 0;
      int num2 = 0;
      if (this.formExit != null)
      {
        Point location = this.Location;
        int x = location.X + (this.Width - this.formExit.Width) / 2;
        location = this.Location;
        int y = location.Y + (this.Height - this.formExit.Height) / 2;
        this.formExit.Location = new Point(x, y);
        this.formProgress.Location = new Point(x, y);
        this.formTimerSaver.Location = new Point(x, y);
      }
      else
      {
        Point location = this.Location;
        num1 = location.X;
        location = this.Location;
        num2 = location.Y;
      }
      int x1 = 20;
      int y1 = (this.panel_RT_Chart.Height - this.pictureBox_RT_Chart_CPS.Height) * 5 / 12;
      this.pictureBox_RT_Chart_CPS.Location = new Point(x1, y1);
      this.pictureBox_RT_Chart_uSv.Location = new Point(x1, y1);
      this.pictureBox_RT_Chart_Time.Location = new Point((this.panel_RT_Chart.Width - this.pictureBox_RT_Chart_Time.Width) / 2 - 370, this.panel_RT_Chart.Height - this.pictureBox_RT_Chart_Time.Height - 10);
    }

    private void InitChartCPSStyle(Chart cpsChart)
    {
      cpsChart.Legends[0].Enabled = false;
      cpsChart.Legends[0].BackColor = Color.FromArgb(207, 214, 229);
      for (int index = 0; index < 6; ++index)
      {
        cpsChart.Series[index].ChartType = SeriesChartType.Spline;
        cpsChart.Series[index].Name = string.Format("User{0}", (object) (index + 1));
        cpsChart.Series[index].BorderWidth = 3;
      }
      cpsChart.Series[6].Name = "Limit";
      cpsChart.Series[6].BorderWidth = 2;
      cpsChart.BackColor = Color.Black;
      cpsChart.Series[0].Color = Color.FromArgb((int) byte.MaxValue, 0, 0);
      cpsChart.Series[1].Color = Color.FromArgb((int) byte.MaxValue, (int) sbyte.MaxValue, 39);
      cpsChart.Series[2].Color = Color.FromArgb(238, 238, 0);
      cpsChart.Series[3].Color = Color.FromArgb(34, 177, 76);
      cpsChart.Series[4].Color = Color.FromArgb(0, 162, 232);
      cpsChart.Series[5].Color = Color.FromArgb(128, 0, (int) byte.MaxValue);
      cpsChart.Series[6].Color = Color.Crimson;
      cpsChart.ChartAreas[0].BackColor = Color.Black;
      cpsChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
      cpsChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
      cpsChart.ChartAreas[0].AxisX.MajorTickMark.LineColor = Color.White;
      cpsChart.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.White;
      cpsChart.ChartAreas[0].AxisX.LineColor = Color.White;
      cpsChart.ChartAreas[0].AxisY.LineColor = Color.White;
      cpsChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      cpsChart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      cpsChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
      cpsChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
      cpsChart.ChartAreas[0].AxisY.LabelStyle.Format = "###,###";
    }

    public Main()
    {
      this.InitializeComponent();
      this.Init();
    }

    private void Main_Load(object sender, EventArgs e)
    {
      try
      {
        this.AutoLaunch();
      }
      catch (Exception ex)
      {
        int num;
        this.UpdateUIThread((MethodInvoker) (() => num = (int) MessageBox.Show("ERROR ::: AutoLaunch(); 실행 중 발생 \r\n\r\n" + ex.ToString())));
      }
    }

    private void Debug(string text, int i)
    {
      if (i != 5 && i != 0)
        return;
      Console.WriteLine(string.Format("{0} : Device{1} {2}", (object) DateTime.Now.ToString("hh:mm:ss.fff"), (object) (i + 1), (object) text));
    }

    private void AutoLaunch()
    {
      for (int index = 0; index < 6; ++index)
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.DummyBTSearch), (object) index);
      this.formProgress.UpdateTitle(string.Format("프로그램 초기화 진행 중", (object[]) Array.Empty<object>()));
      new Thread((ThreadStart) (() =>
      {
        for (int i = 0; i <= 100; i++)
        {
          this.UpdateUIThread((MethodInvoker) (() => this.formProgress.SHOW(i)));
          Thread.Sleep(30);
        }
        this.UpdateUIThread((MethodInvoker) (() => this.formProgress.HIDE()));
      })).Start();
      this.UpdateUIThread((MethodInvoker) (() =>
      {
        this.pictureBox_RT_Chart_uSv.Visible = true;
        this.pictureBox_RT_Chart_Time.Visible = true;
      }));
    }

    private void DummyBTSearch(object Context)
    {
      int index = (int) Context;
      this.Debug("DummyBTSearch Start", index);
      this.MakeSerialPort(index);
      this.BTScan(index);
      this.BLE_State[index] = Main.BLE_STATE.STANDBY;
      this.Debug("DummyBTSearch Finish", index);
    }

    private void MakeSerialPort(int index)
    {
      this.Debug("MakeSerialPort Start", index);
      while (this.BLE_State[index] != Main.BLE_STATE.STANDBY && this.serialPort[index] == null)
      {
        this.Debug("MakeSP - Wating Port StandBy...", index);
        Thread.Sleep(10);
      }
      this.Debug("MakeSP - Making Start", index);
      this.BLE_State[index] = Main.BLE_STATE.OPENING;
      if (this.serialPort[index] != null)
      {
        this.serialPort[index].Close();
        this.serialPort[index].Dispose();
      }
      this.serialPort[index] = (SerialPort) null;
      this.bglib[index] = (BGLib) null;
      this.grpEvt[index] = (GroupFoundEventArgs) null;
      this.handleTable[index] = (List<FindInformationFoundEventArgs>) null;
      this.UpdateUIThread((MethodInvoker) (() =>
      {
        this.serialPort[index] = new SerialPort()
        {
          PortName = "NULL"
        };
        this.bglib[index] = new BGLib();
        this.grpEvt[index] = (GroupFoundEventArgs) null;
        this.handleTable[index] = new List<FindInformationFoundEventArgs>();
      }));
      try
      {
        while (this.serialPort[index] == null)
        {
          this.Debug("MakeSP - Port Waiting...", index);
          Thread.Sleep(10);
        }
        this.serialPort[index] = new SerialPort()
        {
          PortName = this.user[index].COMPort,
          BaudRate = 115200,
          DataBits = 8,
          Parity = Parity.None,
          StopBits = StopBits.One,
          Handshake = Handshake.RequestToSend
        };
        this.serialPort[index].DataReceived += (SerialDataReceivedEventHandler) ((s, e) => this.DataReceived(s, index));
        if (this.isExistComPort(this.serialPort[index].PortName))
        {
          this.Debug("MakeSP - Try port open...", index);
          while (!this.serialPort[index].IsOpen)
          {
            try
            {
              this.serialPort[index].Open();
            }
            catch
            {
              this.Debug("MakeSP - Retry port open...", index);
              Thread.Sleep(50);
            }
          }
        }
        else
        {
          int num = (int) MessageBox.Show(string.Format("User{1} : RPD 센서 통신 채널 설정 오류{0} - \"{2}\" 가 없습니다.{0}{0} ***관리자에게 문의하십시오***{0} [TEL : 070-000-0000]", (object) Environment.NewLine, (object) (index + 1), (object) this.serialPort[index].PortName), "통신 설정 오류", MessageBoxButtons.OK);
          Application.Exit();
        }
        this.bglib[index].BLEEventSystemBoot += (BootEventHandler) ((s, e) => this.SystemBoot(e));
        this.bglib[index].BLEEventGAPScanResponse += (ScanResponseEventHandler) ((s, e) => this.GAPScanResponse(e, index));
        this.bglib[index].BLEEventConnectionStatus += (StatusEventHandler) ((s, e) => this.ConnectionStatus(e, index));
        this.bglib[index].BLEEventATTClientGroupFound += (GroupFoundEventHandler) ((s, e) => this.ATTClientGroupFound(e, index));
        this.bglib[index].BLEEventATTClientFindInformationFound += (FindInformationFoundEventHandler) ((s, e) => this.ATTClientFindInformationFound(e, index));
        this.bglib[index].BLEEventATTClientProcedureCompleted += (ProcedureCompletedEventHandler) ((s, e) => this.ATTClientProcedureCompleted(e, index));
        this.bglib[index].BLEEventATTClientAttributeValue += (AttributeValueEventHandler) ((s, e) => this.ATTClientAttributeValue(e, index));
        while (this.serialPort[index] == null || this.serialPort[index].PortName == "NULL")
        {
          this.Debug("MakeSP - Wating MakeSerialPort Finish...", index);
          Thread.Sleep(10);
        }
        this.Debug("MakeSerialPort Finish", index);
        this.BLE_State[index] = Main.BLE_STATE.OPENED;
      }
      catch (Exception ex)
      {
        ex.ToString();
        string caption = string.Format("USER[{0}] : Serial Port Error", (object) (index + 1));
        int num = (int) MessageBox.Show(string.Format("No Input.\n - Please, Check the COM Port. [User{0}]", (object) (index + 1)) + ex.ToString(), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
      }
    }

    private void BTScan(int index)
    {
      this.Debug("BTScan Start", index);
      while (this.BLE_State[index] != Main.BLE_STATE.OPENED)
      {
        this.Debug("BTScan - Wating port open...", index);
        Thread.Sleep(10);
      }
      this.Debug("BTScan - Scan Start", index);
      this.BluetoothTarget[index].Clear();
      this.BluetoothTarget_Address[index].Clear();
      this.user[index].isStandby = false;
      byte[] cmd1 = this.bglib[index].BLECommandGAPDiscover((byte) 1);
      this.BLE_State[index] = Main.BLE_STATE.SCANNING;
      int num1 = (int) this.bglib[index].SendCommand(this.serialPort[index], cmd1);
      Thread.Sleep(1000);
      byte[] cmd2 = this.bglib[index].BLECommandGAPEndProcedure();
      int num2 = (int) this.bglib[index].SendCommand(this.serialPort[index], cmd2);
      for (int index1 = 0; index1 < 10; ++index1)
      {
        if (this.BLE_State[index] == Main.BLE_STATE.SCANNED)
        {
          this.Debug("BTScan Finish", index);
          break;
        }
        this.Debug("BTScan - BT scanning...", index);
        Thread.Sleep(100);
      }
    }

    private void AttachSensor(object ThreadContext)
    {
      int index = (int) ThreadContext;
      this.Debug("****AttachSensor Start****", index);
      this.MakeSerialPort(index);
      while (this.GetBTLocked(index))
      {
        this.Debug("Wating else BT complete...", index);
        Thread.Sleep(50);
      }
      this.BTScan(index);
      if (this.BLE_State[index] != Main.BLE_STATE.SCANNED)
      {
        int num;
        this.UpdateUIThread((MethodInvoker) (() => num = (int) MessageBox.Show(string.Format("{0}번 기기 탐색 실패\n - 전원 확인 후 다시 활성화 하십시오.\n - 전원이 켜져있다면 활성화 작업을 반복하십시오.", (object) (index + 1)))));
        this.BLE_State[index] = Main.BLE_STATE.STANDBY;
      }
      else
      {
        this.BTConnect(index);
        if (this.BLE_State[index] >= Main.BLE_STATE.CONNECTED)
        {
          this.UpdateUserInfoInterface(index, this.user[index].isUsing, this.user[index].UserName);
          this.user[index].isConnecting = true;
        }
        else
        {
          this.user[index].isUsing = false;
          this.BLE_State[index] = Main.BLE_STATE.STANDBY;
          for (int index1 = 0; index1 < 300; ++index1)
            this.listTimeCPS[index][index1] = "0.0.0. -.0/0";
          this.BTActiveDataCount[index] = 0;
          this.BTActiveState[index] = 6;
          int num;
          this.UpdateUIThread((MethodInvoker) (() => num = (int) MessageBox.Show(string.Format("{0}번 기기 연결 실패\n - 연결 확인 후 다시 활성화 하십시오.\n - 전원이 켜져있다면 활성화 작업을 반복하십시오.", (object) (index + 1)))));
        }
      }
      this.deviceThread.Abort();
      this.formProgress.HIDE();
    }

    private bool GetBTLocked(int index)
    {
      for (int index1 = 0; index1 < 6; ++index1)
      {
        if (index != index1 && this.BLE_State[index1] == Main.BLE_STATE.SCANNING && this.BLE_State[index1] == Main.BLE_STATE.CONNECTING)
          return true;
      }
      return false;
    }

    private bool isExistComPort(string ComPortName)
    {
      foreach (string portName in SerialPort.GetPortNames())
      {
        if (portName.Contains(ComPortName))
          return true;
      }
      return false;
    }

    private void BTConnect(int index)
    {
      string str = this.user[index].BTAddr.Replace(':', ' ');
      for (int index1 = 0; index1 < this.BluetoothTarget[index].Count; ++index1)
      {
        if (this.BluetoothTarget[index][index1].Contains(str))
        {
          this.BLE_State[index] = Main.BLE_STATE.CONNECTING;
          byte[] address = this.BluetoothTarget_Address[index][index1];
          byte addr_type = 1;
          byte[] cmd = this.bglib[index].BLECommandGAPConnectDirect(address, addr_type, (ushort) 32, (ushort) 48, (ushort) 256, (ushort) 0);
          int num = (int) this.bglib[index].SendCommand(this.serialPort[index], cmd);
          this.BTActiveDataCount[index] = 0;
          this.BTActiveState[index] = 0;
          for (int index2 = 0; index2 < 10; ++index2)
          {
            if (this.BLE_State[index] >= Main.BLE_STATE.CONNECTED)
            {
              this.Debug("BTConnect Finish", index);
              break;
            }
            this.Debug("BTConnect - Waiting connect...", index);
            Thread.Sleep(100);
          }
          break;
        }
      }
    }

    private void BTDisconnect(int index)
    {
      if (this.grpEvt[index] == null)
        return;
      byte[] cmd = this.bglib[index].BLECommandConnectionDisconnect(this.grpEvt[index].connection);
      int num = (int) this.bglib[index].SendCommand(this.serialPort[index], cmd);
      this.BLE_State[index] = Main.BLE_STATE.STANDBY;
    }

    private void SetTimer()
    {
      this.tmUpdateGraph.Interval = 985;
      this.tmUpdateGraph.Tick += (EventHandler) ((s, e) =>
      {
        this.UpdateCPSGraph();
        this.UpdateAlert();
      });
      this.tmUpdateGraph.Start();
      for (int userIndex = 0; userIndex < 6; ++userIndex)
      {
        this.SetBTActiveTimer(userIndex);
        this.SetCPSTimer(this.user[userIndex].Hour, this.user[userIndex].Minute, this.user[userIndex].Second, userIndex);
      }
    }

    private void SetBTActiveTimer(int userIndex)
    {
      this.tmBTActive[userIndex] = new System.Windows.Forms.Timer()
      {
        Interval = 4985
      };
      this.tmBTActive[userIndex].Tick += (EventHandler) ((s, e) => this.TmBTActive_Tick(userIndex));
    }

    public void SetCPSTimer(int hour, int min, int sec, int userIndex)
    {
      Main.tmCPSData[userIndex] = new System.Windows.Forms.Timer()
      {
        Interval = hour * 60 * 60 * 1000 + min * 60 * 1000 + sec * 1000 - 15
      };
      this.user[userIndex].TimeSet(hour, min, sec);
      Main.tmCPSData[userIndex].Tick += (EventHandler) ((s, e) => this.tmCPSData_Tick(userIndex));
    }

    private void tmCPSData_Tick(int userIndex)
    {
      this.user[userIndex].Dose_SetTime = this.user[userIndex].Dose_SetTime / 3600.0 / this.user[userIndex].ConvertRate;
      this.user[userIndex].Dose_During += this.user[userIndex].Dose_SetTime;
      this.user[userIndex].SumTotal();
      this.UpdateDoseValue(userIndex);
      this.user[userIndex].WriteSetTime();
      ++this.logWriteCount[userIndex];
      this.user[userIndex].Dose_SetTime = 0.0;
    }

    private void TmBTActive_Tick(int i)
    {
      if (!this.user[i].isUsing)
        return;
      if (this.BTActiveDataCount[i] >= 5)
        this.BTActiveState[i] = 0;
      else if (this.BTActiveDataCount[i] > 0)
        ++this.BTActiveState[i];
      else if (this.BTActiveDataCount[i] == 0)
        this.BTActiveState[i] = 6;
      if (this.BTActiveState[i] == 6)
      {
        if (this.serialPort[i].IsOpen)
          this.BTDisconnect(i);
        this.UpdateUIThread((MethodInvoker) (() =>
        {
          this.user[i].isUsing = false;
          this.user[i].WriteError();
          this.user[i].isConnecting = false;
          this.logWriteCount[i] = 0;
          for (int index = 0; index < 300; ++index)
            this.listTimeCPS[i][index] = "0.0.0. -.0/0";
          this.Label_RT_Value[i].Text = "-";
          Main.tmCPSData[i].Stop();
          this.UpdateUserInfoInterface(i, this.user[i].isUsing, this.user[i].UserName);
          int num = (int) MessageBox.Show(string.Format(" * [ {1} ]\r\n - 사용자{0} 전용 측정기와 통신 해제됨.\r\n - 측정기 전원을 확인하거나 On/Off 후 다시 통신을 시작하십시오.\r\n - 기록하지 않은 로그 정보는 소멸합니다.", (object) (i + 1), (object) this.user[i].UserName));
        }));
      }
      this.BTActiveDataCount[i] = 0;
      this.tmBTActive[i].Stop();
      this.tmBTActive[i].Start();
    }

    private void UpdateUIThread(MethodInvoker method)
    {
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) method);
      else
        method();
    }

    public string ByteArrayToHexString(byte[] ba)
    {
      StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
      foreach (byte num in ba)
        stringBuilder.AppendFormat("{0:x2} ", (object) num);
      return stringBuilder.ToString();
    }

    private void ExitWatch()
    {
      while (true)
      {
        if (!this.formExit.isUserAnswered)
          Thread.Sleep(200);
        else
          break;
      }
      this.UpdateUIThread((MethodInvoker) (() =>
      {
        if (!this.formExit.isUserExit)
          return;
        this.Text = "Good Bye~";
        Thread.Sleep(1000);
        Application.Exit();
      }));
    }

    private void pictureBox_Exit_Click(object sender, EventArgs e)
    {
      this.formExit.SHOW();
      if (this.exitWatch != null)
        this.exitWatch.Abort();
      this.exitWatch = new Thread(new ThreadStart(this.ExitWatch));
      this.exitWatch.Start();
    }

    private void Main_Move(object sender, EventArgs e) => this.Main_Resize(sender, e);

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.exitWatch != null)
        this.exitWatch.Abort();
      for (int index = 0; index < 6; ++index)
      {
        if (this.user[index].isConnecting)
        {
          this.BTDisconnect(index);
          this.serialPort[index].Close();
          this.serialPort[index].Dispose();
        }
        this.serialPort[index] = (SerialPort) null;
        this.bglib[index] = (BGLib) null;
        this.grpEvt[index] = (GroupFoundEventArgs) null;
        this.handleTable[index] = (List<FindInformationFoundEventArgs>) null;
      }
      for (int index = 0; index < 6; ++index)
      {
        if (this.user[index].isConnecting)
        {
          this.user[index].Dose_SetTime = this.user[index].Dose_SetTime / 3600.0 / this.user[index].ConvertRate;
          this.user[index].Dose_During += this.user[index].Dose_SetTime;
          if (this.logWriteCount[index] > 0)
            this.user[index].WriteLastTime();
          this.user[index].SumTotal();
          this.user[index].WriteTotalInfo();
        }
      }
    }

    private void DataReceived(object sender, int userIndex)
    {
      try
      {
        SerialPort serialPort = (SerialPort) sender;
        byte[] buffer = new byte[serialPort.BytesToRead];
        serialPort.Read(buffer, 0, serialPort.BytesToRead);
        for (int index = 0; index < buffer.Length; ++index)
        {
          int num = (int) this.bglib[userIndex].Parse(buffer[index]);
        }
        ++this.BTActiveDataCount[userIndex];
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
    }

    private void SystemBoot(BootEventArgs e) => Console.Write(string.Format("ble_evt_system_boot:" + Environment.NewLine + "\tmajor={0}, minor={1}, patch={2}, build={3}, ll_version={4}, protocol_version={5}, hw={6}" + Environment.NewLine, (object) e.major, (object) e.minor, (object) e.patch, (object) e.build, (object) e.ll_version, (object) e.protocol_version, (object) e.hw));

    private void GAPScanResponse(ScanResponseEventArgs e, int index)
    {
      if (this.BLE_State[index] != Main.BLE_STATE.SCANNING)
        return;
      this.Debug("GAPSR - Scan Start", index);
      this.SaveBluetoothTargetList(e, index);
      string str1 = this.user[index].BTAddr.Replace(':', ' ');
      foreach (string str2 in this.BluetoothTarget[index])
      {
        if (str2.Contains(str1))
        {
          this.Debug("GAPSR - Find Device", index);
          this.BLE_State[index] = Main.BLE_STATE.SCANNED;
        }
      }
    }

    private void SaveBluetoothTargetList(ScanResponseEventArgs e, int index)
    {
      string str1 = "";
      string hexString = this.ByteArrayToHexString(e.sender);
      foreach (string str2 in this.BluetoothTarget[index])
      {
        if (str2 == hexString)
          return;
      }
      string str3 = hexString.TrimEnd((char[]) Array.Empty<char>());
      char[] chArray = new char[1]{ ' ' };
      foreach (string str2 in str3.Split(chArray))
        str1 = str2 + " " + str1;
      if (this.BluetoothTarget[index].Contains(str1))
        return;
      this.BluetoothTarget[index].Add(str1);
      this.BluetoothTarget[index].Sort();
      this.BluetoothTarget_Address[index].Insert(this.BluetoothTarget[index].IndexOf(str1), e.sender);
    }

    private void ConnectionStatus(StatusEventArgs e, int userIndex)
    {
      Console.Write(string.Format("ble_evt_connection_status: connection={0}, flags={1}, address=[ {2}], address_type={3}, conn_interval={4}, timeout={5}, latency={6}, bonding={7}" + Environment.NewLine, (object) e.connection, (object) e.flags, (object) this.ByteArrayToHexString(e.address), (object) e.address_type, (object) e.conn_interval, (object) e.timeout, (object) e.latency, (object) e.bonding));
      if (((int) e.flags & 5) != 5)
        return;
      this.Debug("Pairing Done", userIndex);
      this.BLE_State[userIndex] = Main.BLE_STATE.CONNECTED;
      byte[] cmd = this.bglib[userIndex].BLECommandATTClientReadByGroupType(e.connection, (ushort) 1, ushort.MaxValue, new byte[2]
      {
        (byte) 0,
        (byte) 40
      });
      int num = (int) this.bglib[userIndex].SendCommand(this.serialPort[userIndex], cmd);
    }

    private void ATTClientGroupFound(GroupFoundEventArgs e, int userIndex)
    {
      string.Format("ble_evt_attclient_group_found: connection={0}, start={1}, end={2}, uuid=[ {3}]" + Environment.NewLine, (object) e.connection, (object) e.start, (object) e.end, (object) this.ByteArrayToHexString(e.uuid));
      if (!((IEnumerable<byte>) e.uuid).SequenceEqual<byte>((IEnumerable<byte>) new byte[2]
      {
        (byte) 0,
        (byte) 24
      }))
        return;
      this.BLE_State[userIndex] = Main.BLE_STATE.FINDING_SERVICE;
      if (this.grpEvt[userIndex] != null)
        this.BTDisconnect(userIndex);
      this.grpEvt[userIndex] = e;
    }

    private void ATTClientFindInformationFound(FindInformationFoundEventArgs e, int userIndex)
    {
      string.Format("ble_evt_attclient_find_information_found: connection={0}, chrhandle={1}, uuid=[ {2}]" + Environment.NewLine, (object) e.connection, (object) e.chrhandle, (object) this.ByteArrayToHexString(e.uuid));
      this.handleTable[userIndex].Add(e);
      if (this.grpEvt[userIndex] == null || this.handleTable[userIndex].Count != (int) this.grpEvt[userIndex].end - (int) this.grpEvt[userIndex].start + 1)
        return;
      this.BLE_State[userIndex] = Main.BLE_STATE.RESPONSED_HANDLE;
      if (this.CheckHandleTable(this.handleTable[userIndex].Count, userIndex))
      {
        byte[] cmd = this.bglib[userIndex].BLECommandATTClientAttributeWrite(e.connection, (ushort) 16, new byte[2]
        {
          (byte) 1,
          (byte) 0
        });
        int num = (int) this.bglib[userIndex].SendCommand(this.serialPort[userIndex], cmd);
        this.BLE_State[userIndex] = Main.BLE_STATE.REQUEST_CPS;
      }
      else
        this.UpdateUIThread((MethodInvoker) (() => this.BTDisconnect(userIndex)));
    }

    private bool CheckHandleTable(int count, int userIndex)
    {
      switch (count)
      {
        case 7:
          if (this.handleTable[userIndex][0].uuid[0] == (byte) 0 && this.handleTable[userIndex][0].uuid[1] == (byte) 40 && (this.handleTable[userIndex][1].uuid[0] == (byte) 3 && this.handleTable[userIndex][1].uuid[1] == (byte) 40) && (this.handleTable[userIndex][2].uuid[0] == (byte) 0 && this.handleTable[userIndex][2].uuid[1] == (byte) 42 && (this.handleTable[userIndex][3].uuid[0] == (byte) 3 && this.handleTable[userIndex][3].uuid[1] == (byte) 40)) && (this.handleTable[userIndex][4].uuid[0] == (byte) 1 && this.handleTable[userIndex][4].uuid[1] == (byte) 42 && (this.handleTable[userIndex][5].uuid[0] == (byte) 3 && this.handleTable[userIndex][5].uuid[1] == (byte) 40) && this.handleTable[userIndex][6].uuid[0] == (byte) 4) && this.handleTable[userIndex][6].uuid[1] == (byte) 42)
            return true;
          break;
        case 9:
          if (this.handleTable[userIndex][0].uuid[0] == (byte) 0 && this.handleTable[userIndex][0].uuid[1] == (byte) 40 && (this.handleTable[userIndex][1].uuid[0] == (byte) 3 && this.handleTable[userIndex][1].uuid[1] == (byte) 40) && (this.handleTable[userIndex][2].uuid[0] == (byte) 0 && this.handleTable[userIndex][2].uuid[1] == (byte) 42 && (this.handleTable[userIndex][3].uuid[0] == (byte) 3 && this.handleTable[userIndex][3].uuid[1] == (byte) 40)) && (this.handleTable[userIndex][4].uuid[0] == (byte) 1 && this.handleTable[userIndex][4].uuid[1] == (byte) 42 && (this.handleTable[userIndex][5].uuid[0] == (byte) 3 && this.handleTable[userIndex][5].uuid[1] == (byte) 40) && (this.handleTable[userIndex][6].uuid[0] == (byte) 4 && this.handleTable[userIndex][6].uuid[1] == (byte) 42 && (this.handleTable[userIndex][7].uuid[0] == (byte) 3 && this.handleTable[userIndex][7].uuid[1] == (byte) 40))) && this.handleTable[userIndex][8].uuid[0] == (byte) 166 && this.handleTable[userIndex][8].uuid[1] == (byte) 42)
            return true;
          break;
      }
      return false;
    }

    private void ATTClientProcedureCompleted(ProcedureCompletedEventArgs e, int userIndex)
    {
      if (this.grpEvt[userIndex] == null || this.BLE_State[userIndex] != Main.BLE_STATE.FINDING_SERVICE)
        return;
      this.BLE_State[userIndex] = Main.BLE_STATE.FOUND_SERVICE;
      byte[] information = this.bglib[userIndex].BLECommandATTClientFindInformation(e.connection, this.grpEvt[userIndex].start, this.grpEvt[userIndex].end);
      int num = (int) this.bglib[userIndex].SendCommand(this.serialPort[userIndex], information);
      this.BLE_State[userIndex] = Main.BLE_STATE.REQUEST_HANDLE;
    }

    private void ATTClientAttributeValue(AttributeValueEventArgs e, int userIndex)
    {
      if (this.BLE_State[userIndex] == Main.BLE_STATE.REQUEST_CPS)
        this.BLE_State[userIndex] = Main.BLE_STATE.LISTENING_CPS;
      if (this.BLE_State[userIndex] != Main.BLE_STATE.LISTENING_CPS)
        return;
      int CPS = 0;
      CPS = ((int) e.value[e.value.Length - 7] - 48) * 100000 + ((int) e.value[e.value.Length - 6] - 48) * 10000 + ((int) e.value[e.value.Length - 5] - 48) * 1000 + ((int) e.value[e.value.Length - 4] - 48) * 100 + ((int) e.value[e.value.Length - 3] - 48) * 10 + ((int) e.value[e.value.Length - 2] - 48);
      this.UpdateUIThread((MethodInvoker) (() =>
      {
        if (this.user[userIndex].isUsing)
        {
          if (this.pictureBox_RT_Chart_CPS.Visible)
          {
            if (CPS >= 1000000)
              this.Label_RT_Value[userIndex].Text = string.Format("{0},{1:D3},{2:D3}", (object) (CPS / 1000000), (object) (CPS % 1000000 / 1000), (object) (CPS % 1000));
            else if (CPS >= 1000)
              this.Label_RT_Value[userIndex].Text = string.Format("{0},{1:D3}", (object) (CPS / 1000), (object) (CPS % 1000));
            else
              this.Label_RT_Value[userIndex].Text = string.Format("{0}", (object) CPS);
          }
          else
          {
            double num = Convert.ToDouble(CPS) / this.user[userIndex].ConvertRate;
            if (num >= 1000000.0)
            {
              int int32 = Convert.ToInt32((double) CPS / this.user[userIndex].ConvertRate);
              this.Label_RT_Value[userIndex].Text = string.Format("{0},{1:D3},{2:D3}", (object) (int32 / 1000000), (object) (int32 % 1000000 / 1000), (object) (int32 % 1000));
            }
            else if (num >= 1000.0)
            {
              int int32 = Convert.ToInt32((double) CPS / this.user[userIndex].ConvertRate);
              this.Label_RT_Value[userIndex].Text = string.Format("{0},{1:D3}", (object) (int32 / 1000), (object) (int32 % 1000));
            }
            else
              this.Label_RT_Value[userIndex].Text = string.Format("{0:F2}", (object) num);
          }
        }
        else
          this.Label_RT_Value[userIndex].Text = "-";
        this.user[userIndex].Dose_SetTime += (double) CPS;
        if (!this.user[userIndex].isConnecting)
        {
          this.user[userIndex].WriteConnect();
          Main.tmCPSData[userIndex].Start();
          this.tmBTActive[userIndex].Start();
          this.UpdateDoseValue(userIndex);
        }
        DateTime now = DateTime.Now;
        object[] objArray = new object[6]
        {
          (object) now.Year,
          (object) now.Month,
          (object) now.Day,
          (object) now.Hour,
          (object) now.Minute,
          (object) now.Second
        };
        this.listTimeCPS[userIndex].Add(string.Format("{0:d4}.{1:d2}.{2:d2}. {3:d2}:{4:d2}:{5:d2}", objArray) + "/" + (object) CPS);
        this.listTimeCPS[userIndex].RemoveAt(0);
        if (this.user[userIndex].CPSDataCount >= 300)
          return;
        ++this.user[userIndex].CPSDataCount;
      }));
    }

    private void UpdateCPSGraph()
    {
      int alarm = 0;
      int result = 0;
      if (this.CheckMaxCPS() > this.critical[3])
        result = 1;
      else if (this.CheckMaxCPS() > this.warning[3])
        result = 2;
      this.UpdateUIThread((MethodInvoker) (() =>
      {
        this.chart_RT_CPSChart.Series[0].Points.Clear();
        this.chart_RT_CPSChart.Series[1].Points.Clear();
        this.chart_RT_CPSChart.Series[2].Points.Clear();
        this.chart_RT_CPSChart.Series[3].Points.Clear();
        this.chart_RT_CPSChart.Series[4].Points.Clear();
        this.chart_RT_CPSChart.Series[5].Points.Clear();
        this.chart_RT_CPSChart.Series[6].Points.Clear();
        if (result <= 0)
          return;
        switch (result)
        {
          case 1:
            alarm = this.critical[3];
            this.chart_RT_CPSChart.Series[6].Color = Color.Crimson;
            break;
          case 2:
            alarm = this.warning[3];
            this.chart_RT_CPSChart.Series[6].Color = Color.Yellow;
            break;
          default:
            alarm = 0;
            this.chart_RT_CPSChart.Series[6].Color = Color.White;
            break;
        }
      }));
      int index1 = -1;
      for (int index2 = 0; index2 < 6; ++index2)
      {
        if (this.user[index2].isUsing)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 > -1)
      {
        for (int index2 = 0; index2 < 6; ++index2)
        {
          if (this.user[index2].isUsing && this.user[index1].CPSDataCount < this.user[index2].CPSDataCount)
            index1 = index2;
        }
      }
      for (int j = 0; j < 300; j++)
      {
        if (index1 > -1)
        {
          string time = this.listTimeCPS[index1][j].Split('.')[3].Trim().Split('/')[0];
          if (this.pictureBox_RT_Chart_CPS.Visible)
            this.UpdateUIThread((MethodInvoker) (() =>
            {
              for (int index2 = 0; index2 < 6; ++index2)
              {
                if (this.user[index2].isUsing)
                {
                  int int32 = Convert.ToInt32(this.listTimeCPS[index2][j].Split('/')[1]);
                  this.chart_RT_CPSChart.Series[index2].Points.AddXY((object) time, (object) int32);
                }
              }
              this.chart_RT_CPSChart.Series[6].Points.AddXY((object) time, (object) alarm);
            }));
          else
            this.UpdateUIThread((MethodInvoker) (() =>
            {
              for (int index2 = 0; index2 < 6; ++index2)
              {
                if (this.user[index2].isUsing)
                {
                  int int32 = Convert.ToInt32(this.listTimeCPS[index2][j].Split('/')[1]);
                  this.chart_RT_CPSChart.Series[index2].Points.AddXY((object) time, (object) ((double) int32 / this.user[index2].ConvertRate));
                }
              }
              this.chart_RT_CPSChart.Series[6].Points.AddXY((object) time, (object) ((double) alarm / this.user[0].ConvertRate));
            }));
        }
        else
          this.UpdateUIThread(closure_1 ?? (closure_1 = (MethodInvoker) (() => this.chart_RT_CPSChart.Series[0].Points.AddXY((object) "-", (object) 0))));
      }
    }

    private int CheckCPS(int index)
    {
      int int32 = Convert.ToInt32(this.listTimeCPS[index][299].Split('/')[1]);
      if (this.critical[3] < int32)
        return 2;
      return this.warning[3] < int32 ? 1 : 0;
    }

    private int CheckMaxCPS()
    {
      int num = 0;
      for (int index1 = 0; index1 < 6; ++index1)
      {
        for (int index2 = 0; index2 < 300; ++index2)
        {
          int int32 = Convert.ToInt32(this.listTimeCPS[index1][index2].Split('/')[1]);
          if (num < int32)
            num = int32;
        }
      }
      return num;
    }

    private int CheckDose(int index, int day)
    {
      double num = 0.0;
      switch (day)
      {
        case 0:
          num = this.user[index].Dose_Day;
          break;
        case 1:
          num = this.user[index].Dose_Month;
          break;
        case 2:
          num = this.user[index].Dose_Year;
          break;
      }
      if ((double) this.critical[day] < num)
        return 2;
      return (double) this.warning[day] < num ? 1 : 0;
    }

    private void RT_Check_DoubleClick(int index)
    {
      if (this.formProgress == null || this.formProgress.isShowing)
        return;
      if (!this.user[index].isConnecting)
      {
        this.user[index].isUsing = true;
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.AttachSensor), (object) index);
        this.formProgress.UpdateTitle(string.Format("[{1}] 사용자{0} : 전용 측정기 탐색", (object) (index + 1), (object) this.user[index].UserName));
        this.deviceThread = new Thread((ThreadStart) (() =>
        {
          for (int i = 0; i <= 100; i++)
          {
            this.UpdateUIThread((MethodInvoker) (() => this.formProgress.SHOW(i)));
            Thread.Sleep(30);
          }
          this.UpdateUIThread((MethodInvoker) (() => this.formProgress.HIDE()));
        }));
        this.deviceThread.Start();
      }
      else
        this.user[index].isUsing = !this.user[index].isUsing;
    }

    private void Timer_Reset(int userIndex)
    {
      ++this.logWriteCount[userIndex];
      Main.tmCPSData[userIndex].Stop();
      Main.tmCPSData[userIndex].Start();
    }

    private void User_Log_Interval(int userIndex)
    {
      if (this.user[userIndex].isConnecting)
      {
        int num = (int) MessageBox.Show("사용자 " + this.user[userIndex].UserName + "은 측정기와 통신 중입니다.\n통신 중에는 기록 주기를 변경하실 수 없습니다.", "Timer Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        this.formTimerSaver.Show(this.user[userIndex].UserName, this.user[userIndex].Hour, this.user[userIndex].Minute, this.user[userIndex].Second);
        this.user[userIndex].TimeSet(this.formTimerSaver.Hour, this.formTimerSaver.Minute, this.formTimerSaver.Second);
        this.SetCPSTimer(this.user[userIndex].Hour, this.user[userIndex].Minute, this.user[userIndex].Second, userIndex);
      }
    }

    private void pictureBox_RT_Chart_CPS_DoubleClick(object sender, EventArgs e)
    {
      this.pictureBox_RT_Chart_CPS.Visible = false;
      this.pictureBox_RT_Chart_uSv.Visible = true;
    }

    private void pictureBox_RT_Chart_uSV_DoubleClick(object sender, EventArgs e)
    {
      this.pictureBox_RT_Chart_CPS.Visible = true;
      this.pictureBox_RT_Chart_uSv.Visible = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Main));
      ChartArea chartArea = new ChartArea();
      Legend legend = new Legend();
      Series series1 = new Series();
      Series series2 = new Series();
      Series series3 = new Series();
      Series series4 = new Series();
      Series series5 = new Series();
      Series series6 = new Series();
      Series series7 = new Series();
      this.tableLayoutPanel_Main = new TableLayoutPanel();
      this.c1DockingTab_Main = new C1DockingTab();
      this.c1DockingTabPage_RT = new C1DockingTabPage();
      this.tableLayoutPanel_RT_Main = new TableLayoutPanel();
      this.tableLayoutPanel_RT_Head2 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD6 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD6_Check = new TableLayoutPanel();
      this.panel_RT_TLD6_Check = new Panel();
      this.pictureBox_RT_TLD6_Check = new PictureBox();
      this.panel_RT_TLD6_UserName = new Panel();
      this.label_RT_TLD6_UserName = new Label();
      this.panel_RT_TLD6_Value = new Panel();
      this.label_RT_TLD6_Value = new Label();
      this.tableLayoutPanel_RT_TLD4 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD4_Check = new TableLayoutPanel();
      this.panel_RT_TLD4_Check = new Panel();
      this.pictureBox_RT_TLD4_Check = new PictureBox();
      this.panel_RT_TLD4_UserName = new Panel();
      this.label_RT_TLD4_UserName = new Label();
      this.panel_RT_TLD4_Value = new Panel();
      this.label_RT_TLD4_Value = new Label();
      this.tableLayoutPanel_RT_TLD5 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD5_Check = new TableLayoutPanel();
      this.panel_RT_TLD5_UserName = new Panel();
      this.label_RT_TLD5_UserName = new Label();
      this.panel_RT_TLD5_Check = new Panel();
      this.pictureBox_RT_TLD5_Check = new PictureBox();
      this.panel_RT_TLD5_Value = new Panel();
      this.label_RT_TLD5_Value = new Label();
      this.panel_RT_Chart = new Panel();
      this.pictureBox_RT_Chart_Time = new PictureBox();
      this.pictureBox_RT_Chart_uSv = new PictureBox();
      this.pictureBox_RT_Chart_CPS = new PictureBox();
      this.chart_RT_CPSChart = new Chart();
      this.tableLayoutPanel_RT_Head1 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD3 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD3_Check = new TableLayoutPanel();
      this.panel_RT_TLD3_Check = new Panel();
      this.pictureBox_RT_TLD3_Check = new PictureBox();
      this.panel_RT_TLD3_UserName = new Panel();
      this.label_RT_TLD3_UserName = new Label();
      this.panel_RT_TLD3_Value = new Panel();
      this.label_RT_TLD3_Value = new Label();
      this.tableLayoutPanel_RT_TLD2 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD2_Check = new TableLayoutPanel();
      this.panel_RT_TLD2_UserName = new Panel();
      this.label_RT_TLD2_UserName = new Label();
      this.panel_RT_TLD2_Check = new Panel();
      this.pictureBox_RT_TLD2_Check = new PictureBox();
      this.panel_RT_TLD2_Value = new Panel();
      this.label_RT_TLD2_Value = new Label();
      this.tableLayoutPanel_RT_TLD1 = new TableLayoutPanel();
      this.tableLayoutPanel_RT_TLD1_Check = new TableLayoutPanel();
      this.panel_RT_TLD1_UserName = new Panel();
      this.label_RT_TLD1_UserName = new Label();
      this.panel_RT_TLD1_Check = new Panel();
      this.pictureBox_RT_TLD1_Check = new PictureBox();
      this.panel_RT_TLD1_Value = new Panel();
      this.label_RT_TLD1_Value = new Label();
      this.c1DockingTabPage_Settings = new C1DockingTabPage();
      this.tableLayoutPanel_Config_Main = new TableLayoutPanel();
      this.tableLayoutPanel_Config_Under = new TableLayoutPanel();
      this.tableLayoutPanel_Config_WarningCriticalTitle = new TableLayoutPanel();
      this.panel_Config_criticalDay = new Panel();
      this.label_Config_criticalDay = new Label();
      this.panel_Config_Critical_Name = new Panel();
      this.label_Config_Critical_Name = new Label();
      this.panel_Config_Warning_Name = new Panel();
      this.label_Config_Warning_Name = new Label();
      this.panel_Config_criticalMonth = new Panel();
      this.label_Config_criticalMonth = new Label();
      this.panel_Config_warningMonth = new Panel();
      this.label_Config_warningMonth = new Label();
      this.panel_Config_warningYear = new Panel();
      this.label_Config_warningYear = new Label();
      this.panel_Config_criticalYear = new Panel();
      this.label_Config_criticalYear = new Label();
      this.panel_Config_warningTick = new Panel();
      this.label_Config_warningTick = new Label();
      this.panel_Config_criticalTick = new Panel();
      this.label_Config_criticalTick = new Label();
      this.panel_Config_warningDay = new Panel();
      this.label_Config_warningDay = new Label();
      this.label_Config_WarningCriticalTitle_Year = new Label();
      this.label_Config_WarningCriticalTitle_Month = new Label();
      this.label_Config_WarningCriticalTitle_Day = new Label();
      this.label_Config_WarningCriticalTitle_Tick = new Label();
      this.tableLayoutPanel_Config_UserTitle = new TableLayoutPanel();
      this.pictureBox_user6_timer = new PictureBox();
      this.panel_Config_User4_Name = new Panel();
      this.label_Config_User4_Name = new Label();
      this.panel_Config_User4_day = new Panel();
      this.label_Config_User4_day = new Label();
      this.panel_Config_User4_month = new Panel();
      this.label_Config_User4_month = new Label();
      this.panel_Config_User4_year = new Panel();
      this.label_Config_User4_year = new Label();
      this.panel_Config_User4_BTAddr = new Panel();
      this.label_Config_User4_BTAddr = new Label();
      this.panel_Config_User4_COM = new Panel();
      this.label_Config_User4_COM = new Label();
      this.panel_Config_User5_Name = new Panel();
      this.label_Config_User5_Name = new Label();
      this.panel_Config_User5_day = new Panel();
      this.label_Config_User5_day = new Label();
      this.panel_Config_User5_month = new Panel();
      this.label_Config_User5_month = new Label();
      this.panel_Config_User5_year = new Panel();
      this.label_Config_User5_year = new Label();
      this.panel_Config_User5_BTAddr = new Panel();
      this.label_Config_User5_BTAddr = new Label();
      this.panel_Config_User5_COM = new Panel();
      this.label_Config_User5_COM = new Label();
      this.panel_Config_User6_Name = new Panel();
      this.label_Config_User6_Name = new Label();
      this.panel_Config_User6_day = new Panel();
      this.label_Config_User6_day = new Label();
      this.panel_Config_User6_month = new Panel();
      this.label_Config_User6_month = new Label();
      this.panel_Config_User6_year = new Panel();
      this.label_Config_User6_year = new Label();
      this.panel_Config_User6_BTAddr = new Panel();
      this.label_Config_User6_BTAddr = new Label();
      this.panel_Config_User6_COM = new Panel();
      this.label_Config_User6_COM = new Label();
      this.panel_Config_User3_COM = new Panel();
      this.label_Config_User3_COM = new Label();
      this.panel_Config_User3_Name = new Panel();
      this.label_Config_User3_Name = new Label();
      this.panel_Config_User3_BTAddr = new Panel();
      this.label_Config_User3_BTAddr = new Label();
      this.pictureBox_user2_timer = new PictureBox();
      this.panel_Config_User3_year = new Panel();
      this.label_Config_User3_year = new Label();
      this.pictureBox_user1_timer = new PictureBox();
      this.panel_Config_User3_month = new Panel();
      this.label_Config_User3_month = new Label();
      this.panel_Config_User2_COM = new Panel();
      this.label_Config_User2_COM = new Label();
      this.panel_Config_User3_day = new Panel();
      this.label_Config_User3_day = new Label();
      this.panel_Config_User2_BTAddr = new Panel();
      this.label_Config_User2_BTAddr = new Label();
      this.panel_Config_User2_Name = new Panel();
      this.label_Config_User2_Name = new Label();
      this.panel_Config_User1_COM = new Panel();
      this.label_Config_User1_COM = new Label();
      this.pictureBox_user3_timer = new PictureBox();
      this.panel_Config_User2_year = new Panel();
      this.label_Config_User2_year = new Label();
      this.panel_Config_User1_Name = new Panel();
      this.label_Config_User1_Name = new Label();
      this.panel_Config_User2_month = new Panel();
      this.label_Config_User2_month = new Label();
      this.panel_Config_User1_BTAddr = new Panel();
      this.label_Config_User1_BTAddr = new Label();
      this.panel_Config_User2_day = new Panel();
      this.label_Config_User2_day = new Label();
      this.panel_Config_User1_year = new Panel();
      this.label_Config_User1_year = new Label();
      this.panel_Config_User1_month = new Panel();
      this.label_Config_User1_month = new Label();
      this.panel_Config_User1_day = new Panel();
      this.label_Config_User1_day = new Label();
      this.label_Config_Title_Name = new Label();
      this.label_Config_Title_day = new Label();
      this.label_Config_Title_month = new Label();
      this.label_Config_Title_COM = new Label();
      this.label_Config_Title_BTAddr = new Label();
      this.label_Config_Title_year = new Label();
      this.pictureBox_user4_timer = new PictureBox();
      this.pictureBox_user5_timer = new PictureBox();
      this.tableLayoutPanel_TopMenu = new TableLayoutPanel();
      this.pictureBox_CompanyLogo = new PictureBox();
      this.pictureBox_Exit = new PictureBox();
      this.pictureBox_ProductName = new PictureBox();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.label1 = new Label();
      this.tableLayoutPanel_Main.SuspendLayout();
      ((ISupportInitialize) this.c1DockingTab_Main).BeginInit();
      this.c1DockingTab_Main.SuspendLayout();
      this.c1DockingTabPage_RT.SuspendLayout();
      this.tableLayoutPanel_RT_Main.SuspendLayout();
      this.tableLayoutPanel_RT_Head2.SuspendLayout();
      this.tableLayoutPanel_RT_TLD6.SuspendLayout();
      this.tableLayoutPanel_RT_TLD6_Check.SuspendLayout();
      this.panel_RT_TLD6_Check.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_RT_TLD6_Check).BeginInit();
      this.panel_RT_TLD6_UserName.SuspendLayout();
      this.panel_RT_TLD6_Value.SuspendLayout();
      this.tableLayoutPanel_RT_TLD4.SuspendLayout();
      this.tableLayoutPanel_RT_TLD4_Check.SuspendLayout();
      this.panel_RT_TLD4_Check.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_RT_TLD4_Check).BeginInit();
      this.panel_RT_TLD4_UserName.SuspendLayout();
      this.panel_RT_TLD4_Value.SuspendLayout();
      this.tableLayoutPanel_RT_TLD5.SuspendLayout();
      this.tableLayoutPanel_RT_TLD5_Check.SuspendLayout();
      this.panel_RT_TLD5_UserName.SuspendLayout();
      this.panel_RT_TLD5_Check.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_RT_TLD5_Check).BeginInit();
      this.panel_RT_TLD5_Value.SuspendLayout();
      this.panel_RT_Chart.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_RT_Chart_Time).BeginInit();
      ((ISupportInitialize) this.pictureBox_RT_Chart_uSv).BeginInit();
      ((ISupportInitialize) this.pictureBox_RT_Chart_CPS).BeginInit();
      this.chart_RT_CPSChart.BeginInit();
      this.tableLayoutPanel_RT_Head1.SuspendLayout();
      this.tableLayoutPanel_RT_TLD3.SuspendLayout();
      this.tableLayoutPanel_RT_TLD3_Check.SuspendLayout();
      this.panel_RT_TLD3_Check.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_RT_TLD3_Check).BeginInit();
      this.panel_RT_TLD3_UserName.SuspendLayout();
      this.panel_RT_TLD3_Value.SuspendLayout();
      this.tableLayoutPanel_RT_TLD2.SuspendLayout();
      this.tableLayoutPanel_RT_TLD2_Check.SuspendLayout();
      this.panel_RT_TLD2_UserName.SuspendLayout();
      this.panel_RT_TLD2_Check.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_RT_TLD2_Check).BeginInit();
      this.panel_RT_TLD2_Value.SuspendLayout();
      this.tableLayoutPanel_RT_TLD1.SuspendLayout();
      this.tableLayoutPanel_RT_TLD1_Check.SuspendLayout();
      this.panel_RT_TLD1_UserName.SuspendLayout();
      this.panel_RT_TLD1_Check.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_RT_TLD1_Check).BeginInit();
      this.panel_RT_TLD1_Value.SuspendLayout();
      this.c1DockingTabPage_Settings.SuspendLayout();
      this.tableLayoutPanel_Config_Main.SuspendLayout();
      this.tableLayoutPanel_Config_Under.SuspendLayout();
      this.tableLayoutPanel_Config_WarningCriticalTitle.SuspendLayout();
      this.panel_Config_criticalDay.SuspendLayout();
      this.panel_Config_Critical_Name.SuspendLayout();
      this.panel_Config_Warning_Name.SuspendLayout();
      this.panel_Config_criticalMonth.SuspendLayout();
      this.panel_Config_warningMonth.SuspendLayout();
      this.panel_Config_warningYear.SuspendLayout();
      this.panel_Config_criticalYear.SuspendLayout();
      this.panel_Config_warningTick.SuspendLayout();
      this.panel_Config_criticalTick.SuspendLayout();
      this.panel_Config_warningDay.SuspendLayout();
      this.tableLayoutPanel_Config_UserTitle.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_user6_timer).BeginInit();
      this.panel_Config_User4_Name.SuspendLayout();
      this.panel_Config_User4_day.SuspendLayout();
      this.panel_Config_User4_month.SuspendLayout();
      this.panel_Config_User4_year.SuspendLayout();
      this.panel_Config_User4_BTAddr.SuspendLayout();
      this.panel_Config_User4_COM.SuspendLayout();
      this.panel_Config_User5_Name.SuspendLayout();
      this.panel_Config_User5_day.SuspendLayout();
      this.panel_Config_User5_month.SuspendLayout();
      this.panel_Config_User5_year.SuspendLayout();
      this.panel_Config_User5_BTAddr.SuspendLayout();
      this.panel_Config_User5_COM.SuspendLayout();
      this.panel_Config_User6_Name.SuspendLayout();
      this.panel_Config_User6_day.SuspendLayout();
      this.panel_Config_User6_month.SuspendLayout();
      this.panel_Config_User6_year.SuspendLayout();
      this.panel_Config_User6_BTAddr.SuspendLayout();
      this.panel_Config_User6_COM.SuspendLayout();
      this.panel_Config_User3_COM.SuspendLayout();
      this.panel_Config_User3_Name.SuspendLayout();
      this.panel_Config_User3_BTAddr.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_user2_timer).BeginInit();
      this.panel_Config_User3_year.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_user1_timer).BeginInit();
      this.panel_Config_User3_month.SuspendLayout();
      this.panel_Config_User2_COM.SuspendLayout();
      this.panel_Config_User3_day.SuspendLayout();
      this.panel_Config_User2_BTAddr.SuspendLayout();
      this.panel_Config_User2_Name.SuspendLayout();
      this.panel_Config_User1_COM.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_user3_timer).BeginInit();
      this.panel_Config_User2_year.SuspendLayout();
      this.panel_Config_User1_Name.SuspendLayout();
      this.panel_Config_User2_month.SuspendLayout();
      this.panel_Config_User1_BTAddr.SuspendLayout();
      this.panel_Config_User2_day.SuspendLayout();
      this.panel_Config_User1_year.SuspendLayout();
      this.panel_Config_User1_month.SuspendLayout();
      this.panel_Config_User1_day.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_user4_timer).BeginInit();
      ((ISupportInitialize) this.pictureBox_user5_timer).BeginInit();
      this.tableLayoutPanel_TopMenu.SuspendLayout();
      ((ISupportInitialize) this.pictureBox_CompanyLogo).BeginInit();
      ((ISupportInitialize) this.pictureBox_Exit).BeginInit();
      ((ISupportInitialize) this.pictureBox_ProductName).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.tableLayoutPanel_Main.BackColor = Color.FromArgb(224, 224, 224);
      this.tableLayoutPanel_Main.ColumnCount = 1;
      this.tableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_Main.Controls.Add((Control) this.c1DockingTab_Main, 0, 1);
      this.tableLayoutPanel_Main.Controls.Add((Control) this.tableLayoutPanel_TopMenu, 0, 0);
      this.tableLayoutPanel_Main.Location = new Point(12, 12);
      this.tableLayoutPanel_Main.Margin = new Padding(0);
      this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
      this.tableLayoutPanel_Main.RowCount = 3;
      this.tableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 15.80189f));
      this.tableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 84.19811f));
      this.tableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 1f));
      this.tableLayoutPanel_Main.Size = new Size(1039, 627);
      this.tableLayoutPanel_Main.TabIndex = 0;
      this.c1DockingTab_Main.Alignment = TabAlignment.Left;
      this.c1DockingTab_Main.Controls.Add((Control) this.c1DockingTabPage_RT);
      this.c1DockingTab_Main.Controls.Add((Control) this.c1DockingTabPage_Settings);
      this.c1DockingTab_Main.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.c1DockingTab_Main.Location = new Point(0, 98);
      this.c1DockingTab_Main.Margin = new Padding(0);
      this.c1DockingTab_Main.Name = "c1DockingTab_Main";
      this.c1DockingTab_Main.Size = new Size(1039, 527);
      this.c1DockingTab_Main.TabIndex = 1;
      this.c1DockingTab_Main.TabsSpacing = 5;
      this.c1DockingTab_Main.TabStyle = TabStyleEnum.Office2010;
      this.c1DockingTab_Main.VisualStyle = VisualStyle.Office2010Silver;
      this.c1DockingTab_Main.VisualStyleBase = VisualStyle.Office2010Silver;
      this.c1DockingTabPage_RT.BackColor = Color.FromArgb(224, 224, 224);
      this.c1DockingTabPage_RT.Controls.Add((Control) this.tableLayoutPanel_RT_Main);
      this.c1DockingTabPage_RT.Font = new Font("Microsoft Sans Serif", 20f);
      this.c1DockingTabPage_RT.Location = new Point(44, 1);
      this.c1DockingTabPage_RT.Margin = new Padding(0);
      this.c1DockingTabPage_RT.Name = "c1DockingTabPage_RT";
      this.c1DockingTabPage_RT.Size = new Size(994, 525);
      this.c1DockingTabPage_RT.TabBackColor = Color.FromArgb(224, 224, 224);
      this.c1DockingTabPage_RT.TabIndex = 0;
      this.c1DockingTabPage_RT.Text = "Real Time";
      this.tableLayoutPanel_RT_Main.ColumnCount = 1;
      this.tableLayoutPanel_RT_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_Main.Controls.Add((Control) this.tableLayoutPanel_RT_Head2, 0, 1);
      this.tableLayoutPanel_RT_Main.Controls.Add((Control) this.panel_RT_Chart, 0, 2);
      this.tableLayoutPanel_RT_Main.Controls.Add((Control) this.tableLayoutPanel_RT_Head1, 0, 0);
      this.tableLayoutPanel_RT_Main.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_Main.Margin = new Padding(0);
      this.tableLayoutPanel_RT_Main.Name = "tableLayoutPanel_RT_Main";
      this.tableLayoutPanel_RT_Main.RowCount = 3;
      this.tableLayoutPanel_RT_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel_RT_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel_RT_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel_RT_Main.Size = new Size(994, 525);
      this.tableLayoutPanel_RT_Main.TabIndex = 0;
      this.tableLayoutPanel_RT_Head2.ColumnCount = 3;
      this.tableLayoutPanel_RT_Head2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel_RT_Head2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel_RT_Head2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel_RT_Head2.Controls.Add((Control) this.tableLayoutPanel_RT_TLD6, 2, 0);
      this.tableLayoutPanel_RT_Head2.Controls.Add((Control) this.tableLayoutPanel_RT_TLD4, 0, 0);
      this.tableLayoutPanel_RT_Head2.Controls.Add((Control) this.tableLayoutPanel_RT_TLD5, 1, 0);
      this.tableLayoutPanel_RT_Head2.Location = new Point(0, 131);
      this.tableLayoutPanel_RT_Head2.Margin = new Padding(0);
      this.tableLayoutPanel_RT_Head2.Name = "tableLayoutPanel_RT_Head2";
      this.tableLayoutPanel_RT_Head2.RowCount = 1;
      this.tableLayoutPanel_RT_Head2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_Head2.Size = new Size(994, 131);
      this.tableLayoutPanel_RT_Head2.TabIndex = 3;
      this.tableLayoutPanel_RT_TLD6.ColumnCount = 1;
      this.tableLayoutPanel_RT_TLD6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD6.Controls.Add((Control) this.tableLayoutPanel_RT_TLD6_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD6.Controls.Add((Control) this.panel_RT_TLD6_Value, 0, 1);
      this.tableLayoutPanel_RT_TLD6.Location = new Point(662, 0);
      this.tableLayoutPanel_RT_TLD6.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD6.Name = "tableLayoutPanel_RT_TLD6";
      this.tableLayoutPanel_RT_TLD6.RowCount = 2;
      this.tableLayoutPanel_RT_TLD6.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_RT_TLD6.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      this.tableLayoutPanel_RT_TLD6.Size = new Size(332, 131);
      this.tableLayoutPanel_RT_TLD6.TabIndex = 1;
      this.tableLayoutPanel_RT_TLD6_Check.ColumnCount = 2;
      this.tableLayoutPanel_RT_TLD6_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel_RT_TLD6_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
      this.tableLayoutPanel_RT_TLD6_Check.Controls.Add((Control) this.panel_RT_TLD6_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD6_Check.Controls.Add((Control) this.panel_RT_TLD6_UserName, 1, 0);
      this.tableLayoutPanel_RT_TLD6_Check.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD6_Check.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD6_Check.Name = "tableLayoutPanel_RT_TLD6_Check";
      this.tableLayoutPanel_RT_TLD6_Check.RowCount = 1;
      this.tableLayoutPanel_RT_TLD6_Check.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD6_Check.Size = new Size(332, 52);
      this.tableLayoutPanel_RT_TLD6_Check.TabIndex = 1;
      this.panel_RT_TLD6_Check.BackgroundImage = (Image) componentResourceManager.GetObject("panel_RT_TLD6_Check.BackgroundImage");
      this.panel_RT_TLD6_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel_RT_TLD6_Check.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD6_Check.Controls.Add((Control) this.pictureBox_RT_TLD6_Check);
      this.panel_RT_TLD6_Check.Location = new Point(0, 3);
      this.panel_RT_TLD6_Check.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD6_Check.Name = "panel_RT_TLD6_Check";
      this.panel_RT_TLD6_Check.Size = new Size(63, 49);
      this.panel_RT_TLD6_Check.TabIndex = 3;
      this.pictureBox_RT_TLD6_Check.BackColor = Color.Yellow;
      this.pictureBox_RT_TLD6_Check.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_TLD6_Check.BackgroundImage");
      this.pictureBox_RT_TLD6_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_TLD6_Check.Location = new Point(0, 0);
      this.pictureBox_RT_TLD6_Check.Name = "pictureBox_RT_TLD6_Check";
      this.pictureBox_RT_TLD6_Check.Size = new Size(46, 31);
      this.pictureBox_RT_TLD6_Check.TabIndex = 1;
      this.pictureBox_RT_TLD6_Check.TabStop = false;
      this.pictureBox_RT_TLD6_Check.Visible = false;
      this.panel_RT_TLD6_UserName.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD6_UserName.Controls.Add((Control) this.label_RT_TLD6_UserName);
      this.panel_RT_TLD6_UserName.Location = new Point(66, 3);
      this.panel_RT_TLD6_UserName.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD6_UserName.Name = "panel_RT_TLD6_UserName";
      this.panel_RT_TLD6_UserName.Size = new Size(266, 49);
      this.panel_RT_TLD6_UserName.TabIndex = 2;
      this.label_RT_TLD6_UserName.Font = new Font("Microsoft Sans Serif", 30f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD6_UserName.Location = new Point(-2, 0);
      this.label_RT_TLD6_UserName.Margin = new Padding(0);
      this.label_RT_TLD6_UserName.Name = "label_RT_TLD6_UserName";
      this.label_RT_TLD6_UserName.Size = new Size(183, 45);
      this.label_RT_TLD6_UserName.TabIndex = 0;
      this.label_RT_TLD6_UserName.Text = "User6";
      this.label_RT_TLD6_UserName.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_RT_TLD6_Value.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD6_Value.Controls.Add((Control) this.label_RT_TLD6_Value);
      this.panel_RT_TLD6_Value.Location = new Point(0, 52);
      this.panel_RT_TLD6_Value.Margin = new Padding(0);
      this.panel_RT_TLD6_Value.Name = "panel_RT_TLD6_Value";
      this.panel_RT_TLD6_Value.Size = new Size(332, 79);
      this.panel_RT_TLD6_Value.TabIndex = 2;
      this.label_RT_TLD6_Value.Font = new Font("Microsoft Sans Serif", 50.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD6_Value.Location = new Point(4, 2);
      this.label_RT_TLD6_Value.Margin = new Padding(0);
      this.label_RT_TLD6_Value.Name = "label_RT_TLD6_Value";
      this.label_RT_TLD6_Value.Size = new Size(270, 59);
      this.label_RT_TLD6_Value.TabIndex = 0;
      this.label_RT_TLD6_Value.Text = "Value";
      this.label_RT_TLD6_Value.TextAlign = ContentAlignment.MiddleCenter;
      this.tableLayoutPanel_RT_TLD4.ColumnCount = 1;
      this.tableLayoutPanel_RT_TLD4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD4.Controls.Add((Control) this.tableLayoutPanel_RT_TLD4_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD4.Controls.Add((Control) this.panel_RT_TLD4_Value, 0, 1);
      this.tableLayoutPanel_RT_TLD4.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD4.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD4.Name = "tableLayoutPanel_RT_TLD4";
      this.tableLayoutPanel_RT_TLD4.RowCount = 2;
      this.tableLayoutPanel_RT_TLD4.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_RT_TLD4.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      this.tableLayoutPanel_RT_TLD4.Size = new Size(330, 131);
      this.tableLayoutPanel_RT_TLD4.TabIndex = 1;
      this.tableLayoutPanel_RT_TLD4_Check.ColumnCount = 2;
      this.tableLayoutPanel_RT_TLD4_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel_RT_TLD4_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
      this.tableLayoutPanel_RT_TLD4_Check.Controls.Add((Control) this.panel_RT_TLD4_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD4_Check.Controls.Add((Control) this.panel_RT_TLD4_UserName, 1, 0);
      this.tableLayoutPanel_RT_TLD4_Check.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD4_Check.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD4_Check.Name = "tableLayoutPanel_RT_TLD4_Check";
      this.tableLayoutPanel_RT_TLD4_Check.RowCount = 1;
      this.tableLayoutPanel_RT_TLD4_Check.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD4_Check.Size = new Size(330, 52);
      this.tableLayoutPanel_RT_TLD4_Check.TabIndex = 1;
      this.panel_RT_TLD4_Check.BackgroundImage = (Image) componentResourceManager.GetObject("panel_RT_TLD4_Check.BackgroundImage");
      this.panel_RT_TLD4_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel_RT_TLD4_Check.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD4_Check.Controls.Add((Control) this.pictureBox_RT_TLD4_Check);
      this.panel_RT_TLD4_Check.Location = new Point(0, 3);
      this.panel_RT_TLD4_Check.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD4_Check.Name = "panel_RT_TLD4_Check";
      this.panel_RT_TLD4_Check.Size = new Size(66, 49);
      this.panel_RT_TLD4_Check.TabIndex = 3;
      this.pictureBox_RT_TLD4_Check.BackColor = SystemColors.Highlight;
      this.pictureBox_RT_TLD4_Check.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_TLD4_Check.BackgroundImage");
      this.pictureBox_RT_TLD4_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_TLD4_Check.Location = new Point(-2, -2);
      this.pictureBox_RT_TLD4_Check.Name = "pictureBox_RT_TLD4_Check";
      this.pictureBox_RT_TLD4_Check.Size = new Size(47, 31);
      this.pictureBox_RT_TLD4_Check.TabIndex = 1;
      this.pictureBox_RT_TLD4_Check.TabStop = false;
      this.pictureBox_RT_TLD4_Check.Visible = false;
      this.panel_RT_TLD4_UserName.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD4_UserName.Controls.Add((Control) this.label_RT_TLD4_UserName);
      this.panel_RT_TLD4_UserName.Location = new Point(66, 3);
      this.panel_RT_TLD4_UserName.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD4_UserName.Name = "panel_RT_TLD4_UserName";
      this.panel_RT_TLD4_UserName.Size = new Size(264, 49);
      this.panel_RT_TLD4_UserName.TabIndex = 2;
      this.label_RT_TLD4_UserName.Font = new Font("Microsoft Sans Serif", 30f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD4_UserName.Location = new Point(1, 0);
      this.label_RT_TLD4_UserName.Margin = new Padding(0);
      this.label_RT_TLD4_UserName.Name = "label_RT_TLD4_UserName";
      this.label_RT_TLD4_UserName.Size = new Size(181, 45);
      this.label_RT_TLD4_UserName.TabIndex = 0;
      this.label_RT_TLD4_UserName.Text = "User4";
      this.label_RT_TLD4_UserName.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_RT_TLD4_Value.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD4_Value.Controls.Add((Control) this.label_RT_TLD4_Value);
      this.panel_RT_TLD4_Value.Location = new Point(0, 52);
      this.panel_RT_TLD4_Value.Margin = new Padding(0);
      this.panel_RT_TLD4_Value.Name = "panel_RT_TLD4_Value";
      this.panel_RT_TLD4_Value.Size = new Size(330, 79);
      this.panel_RT_TLD4_Value.TabIndex = 2;
      this.label_RT_TLD4_Value.Font = new Font("Microsoft Sans Serif", 50.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD4_Value.Location = new Point(3, 2);
      this.label_RT_TLD4_Value.Margin = new Padding(0);
      this.label_RT_TLD4_Value.Name = "label_RT_TLD4_Value";
      this.label_RT_TLD4_Value.Size = new Size(276, 55);
      this.label_RT_TLD4_Value.TabIndex = 0;
      this.label_RT_TLD4_Value.Text = "Value";
      this.label_RT_TLD4_Value.TextAlign = ContentAlignment.MiddleCenter;
      this.tableLayoutPanel_RT_TLD5.ColumnCount = 1;
      this.tableLayoutPanel_RT_TLD5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD5.Controls.Add((Control) this.tableLayoutPanel_RT_TLD5_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD5.Controls.Add((Control) this.panel_RT_TLD5_Value, 0, 1);
      this.tableLayoutPanel_RT_TLD5.Location = new Point(331, 0);
      this.tableLayoutPanel_RT_TLD5.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD5.Name = "tableLayoutPanel_RT_TLD5";
      this.tableLayoutPanel_RT_TLD5.RowCount = 2;
      this.tableLayoutPanel_RT_TLD5.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_RT_TLD5.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      this.tableLayoutPanel_RT_TLD5.Size = new Size(331, 131);
      this.tableLayoutPanel_RT_TLD5.TabIndex = 1;
      this.tableLayoutPanel_RT_TLD5_Check.ColumnCount = 2;
      this.tableLayoutPanel_RT_TLD5_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel_RT_TLD5_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
      this.tableLayoutPanel_RT_TLD5_Check.Controls.Add((Control) this.panel_RT_TLD5_UserName, 1, 0);
      this.tableLayoutPanel_RT_TLD5_Check.Controls.Add((Control) this.panel_RT_TLD5_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD5_Check.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD5_Check.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD5_Check.Name = "tableLayoutPanel_RT_TLD5_Check";
      this.tableLayoutPanel_RT_TLD5_Check.RowCount = 1;
      this.tableLayoutPanel_RT_TLD5_Check.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD5_Check.Size = new Size(331, 52);
      this.tableLayoutPanel_RT_TLD5_Check.TabIndex = 1;
      this.panel_RT_TLD5_UserName.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD5_UserName.Controls.Add((Control) this.label_RT_TLD5_UserName);
      this.panel_RT_TLD5_UserName.Location = new Point(66, 3);
      this.panel_RT_TLD5_UserName.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD5_UserName.Name = "panel_RT_TLD5_UserName";
      this.panel_RT_TLD5_UserName.Size = new Size(264, 49);
      this.panel_RT_TLD5_UserName.TabIndex = 2;
      this.label_RT_TLD5_UserName.Font = new Font("Microsoft Sans Serif", 30f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD5_UserName.Location = new Point(1, 0);
      this.label_RT_TLD5_UserName.Margin = new Padding(0);
      this.label_RT_TLD5_UserName.Name = "label_RT_TLD5_UserName";
      this.label_RT_TLD5_UserName.Size = new Size(181, 45);
      this.label_RT_TLD5_UserName.TabIndex = 0;
      this.label_RT_TLD5_UserName.Text = "User5";
      this.label_RT_TLD5_UserName.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_RT_TLD5_Check.BackgroundImage = (Image) componentResourceManager.GetObject("panel_RT_TLD5_Check.BackgroundImage");
      this.panel_RT_TLD5_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel_RT_TLD5_Check.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD5_Check.Controls.Add((Control) this.pictureBox_RT_TLD5_Check);
      this.panel_RT_TLD5_Check.Location = new Point(0, 3);
      this.panel_RT_TLD5_Check.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD5_Check.Name = "panel_RT_TLD5_Check";
      this.panel_RT_TLD5_Check.Size = new Size(66, 49);
      this.panel_RT_TLD5_Check.TabIndex = 3;
      this.pictureBox_RT_TLD5_Check.BackColor = Color.Red;
      this.pictureBox_RT_TLD5_Check.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_TLD5_Check.BackgroundImage");
      this.pictureBox_RT_TLD5_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_TLD5_Check.Location = new Point(0, 0);
      this.pictureBox_RT_TLD5_Check.Name = "pictureBox_RT_TLD5_Check";
      this.pictureBox_RT_TLD5_Check.Size = new Size(44, 31);
      this.pictureBox_RT_TLD5_Check.TabIndex = 1;
      this.pictureBox_RT_TLD5_Check.TabStop = false;
      this.pictureBox_RT_TLD5_Check.Visible = false;
      this.panel_RT_TLD5_Value.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD5_Value.Controls.Add((Control) this.label_RT_TLD5_Value);
      this.panel_RT_TLD5_Value.Location = new Point(0, 52);
      this.panel_RT_TLD5_Value.Margin = new Padding(0);
      this.panel_RT_TLD5_Value.Name = "panel_RT_TLD5_Value";
      this.panel_RT_TLD5_Value.Size = new Size(330, 79);
      this.panel_RT_TLD5_Value.TabIndex = 2;
      this.label_RT_TLD5_Value.Font = new Font("Microsoft Sans Serif", 50.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD5_Value.Location = new Point(3, 2);
      this.label_RT_TLD5_Value.Margin = new Padding(0);
      this.label_RT_TLD5_Value.Name = "label_RT_TLD5_Value";
      this.label_RT_TLD5_Value.Size = new Size(272, 59);
      this.label_RT_TLD5_Value.TabIndex = 0;
      this.label_RT_TLD5_Value.Text = "Value";
      this.label_RT_TLD5_Value.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_RT_Chart.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_Chart.Controls.Add((Control) this.pictureBox_RT_Chart_Time);
      this.panel_RT_Chart.Controls.Add((Control) this.pictureBox_RT_Chart_uSv);
      this.panel_RT_Chart.Controls.Add((Control) this.pictureBox_RT_Chart_CPS);
      this.panel_RT_Chart.Controls.Add((Control) this.chart_RT_CPSChart);
      this.panel_RT_Chart.Location = new Point(0, 262);
      this.panel_RT_Chart.Margin = new Padding(0);
      this.panel_RT_Chart.Name = "panel_RT_Chart";
      this.panel_RT_Chart.Size = new Size(994, 263);
      this.panel_RT_Chart.TabIndex = 2;
      this.pictureBox_RT_Chart_Time.BackColor = Color.LightGray;
      this.pictureBox_RT_Chart_Time.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_Chart_Time.BackgroundImage");
      this.pictureBox_RT_Chart_Time.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_Chart_Time.Location = new Point(131, 81);
      this.pictureBox_RT_Chart_Time.Margin = new Padding(0);
      this.pictureBox_RT_Chart_Time.Name = "pictureBox_RT_Chart_Time";
      this.pictureBox_RT_Chart_Time.Size = new Size(56, 24);
      this.pictureBox_RT_Chart_Time.TabIndex = 1;
      this.pictureBox_RT_Chart_Time.TabStop = false;
      this.pictureBox_RT_Chart_Time.Visible = false;
      this.pictureBox_RT_Chart_uSv.BackColor = Color.LightGray;
      this.pictureBox_RT_Chart_uSv.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_Chart_uSv.BackgroundImage");
      this.pictureBox_RT_Chart_uSv.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_Chart_uSv.Location = new Point(8, 81);
      this.pictureBox_RT_Chart_uSv.Margin = new Padding(0);
      this.pictureBox_RT_Chart_uSv.Name = "pictureBox_RT_Chart_uSv";
      this.pictureBox_RT_Chart_uSv.Size = new Size(21, 43);
      this.pictureBox_RT_Chart_uSv.TabIndex = 1;
      this.pictureBox_RT_Chart_uSv.TabStop = false;
      this.pictureBox_RT_Chart_uSv.Visible = false;
      this.pictureBox_RT_Chart_uSv.DoubleClick += new EventHandler(this.pictureBox_RT_Chart_uSV_DoubleClick);
      this.pictureBox_RT_Chart_CPS.BackColor = Color.LightGray;
      this.pictureBox_RT_Chart_CPS.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_Chart_CPS.BackgroundImage");
      this.pictureBox_RT_Chart_CPS.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_Chart_CPS.Location = new Point(8, 47);
      this.pictureBox_RT_Chart_CPS.Margin = new Padding(0);
      this.pictureBox_RT_Chart_CPS.Name = "pictureBox_RT_Chart_CPS";
      this.pictureBox_RT_Chart_CPS.Size = new Size(21, 43);
      this.pictureBox_RT_Chart_CPS.TabIndex = 1;
      this.pictureBox_RT_Chart_CPS.TabStop = false;
      this.pictureBox_RT_Chart_CPS.Visible = false;
      this.pictureBox_RT_Chart_CPS.DoubleClick += new EventHandler(this.pictureBox_RT_Chart_CPS_DoubleClick);
      this.chart_RT_CPSChart.BorderlineDashStyle = ChartDashStyle.Solid;
      this.chart_RT_CPSChart.BorderlineWidth = 3;
      chartArea.Name = "ChartArea1";
      this.chart_RT_CPSChart.ChartAreas.Add(chartArea);
      legend.Name = "Legend1";
      this.chart_RT_CPSChart.Legends.Add(legend);
      this.chart_RT_CPSChart.Location = new Point(8, 3);
      this.chart_RT_CPSChart.Margin = new Padding(0);
      this.chart_RT_CPSChart.Name = "chart_RT_CPSChart";
      series1.ChartArea = "ChartArea1";
      series1.ChartType = SeriesChartType.Spline;
      series1.Legend = "Legend1";
      series1.Name = "Series1";
      series2.BorderDashStyle = ChartDashStyle.Dash;
      series2.ChartArea = "ChartArea1";
      series2.ChartType = SeriesChartType.Spline;
      series2.Legend = "Legend1";
      series2.Name = "Series2";
      series3.BorderDashStyle = ChartDashStyle.Dot;
      series3.ChartArea = "ChartArea1";
      series3.ChartType = SeriesChartType.Spline;
      series3.Legend = "Legend1";
      series3.Name = "Series3";
      series4.ChartArea = "ChartArea1";
      series4.ChartType = SeriesChartType.Spline;
      series4.Legend = "Legend1";
      series4.Name = "Series4";
      series5.BorderDashStyle = ChartDashStyle.Dash;
      series5.ChartArea = "ChartArea1";
      series5.ChartType = SeriesChartType.Spline;
      series5.Legend = "Legend1";
      series5.Name = "Series5";
      series6.BorderDashStyle = ChartDashStyle.Dot;
      series6.ChartArea = "ChartArea1";
      series6.ChartType = SeriesChartType.Spline;
      series6.Legend = "Legend1";
      series6.Name = "Series6";
      series7.BorderDashStyle = ChartDashStyle.Dash;
      series7.ChartArea = "ChartArea1";
      series7.ChartType = SeriesChartType.Spline;
      series7.Legend = "Legend1";
      series7.Name = "Series7";
      this.chart_RT_CPSChart.Series.Add(series1);
      this.chart_RT_CPSChart.Series.Add(series2);
      this.chart_RT_CPSChart.Series.Add(series3);
      this.chart_RT_CPSChart.Series.Add(series4);
      this.chart_RT_CPSChart.Series.Add(series5);
      this.chart_RT_CPSChart.Series.Add(series6);
      this.chart_RT_CPSChart.Series.Add(series7);
      this.chart_RT_CPSChart.Size = new Size(709, 189);
      this.chart_RT_CPSChart.TabIndex = 1;
      this.chart_RT_CPSChart.Text = "CPS";
      this.tableLayoutPanel_RT_Head1.ColumnCount = 3;
      this.tableLayoutPanel_RT_Head1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel_RT_Head1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel_RT_Head1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel_RT_Head1.Controls.Add((Control) this.tableLayoutPanel_RT_TLD3, 2, 0);
      this.tableLayoutPanel_RT_Head1.Controls.Add((Control) this.tableLayoutPanel_RT_TLD2, 1, 0);
      this.tableLayoutPanel_RT_Head1.Controls.Add((Control) this.tableLayoutPanel_RT_TLD1, 0, 0);
      this.tableLayoutPanel_RT_Head1.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_Head1.Margin = new Padding(0);
      this.tableLayoutPanel_RT_Head1.Name = "tableLayoutPanel_RT_Head1";
      this.tableLayoutPanel_RT_Head1.RowCount = 1;
      this.tableLayoutPanel_RT_Head1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_Head1.Size = new Size(994, 131);
      this.tableLayoutPanel_RT_Head1.TabIndex = 1;
      this.tableLayoutPanel_RT_TLD3.ColumnCount = 1;
      this.tableLayoutPanel_RT_TLD3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD3.Controls.Add((Control) this.tableLayoutPanel_RT_TLD3_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD3.Controls.Add((Control) this.panel_RT_TLD3_Value, 0, 1);
      this.tableLayoutPanel_RT_TLD3.Location = new Point(662, 0);
      this.tableLayoutPanel_RT_TLD3.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD3.Name = "tableLayoutPanel_RT_TLD3";
      this.tableLayoutPanel_RT_TLD3.RowCount = 2;
      this.tableLayoutPanel_RT_TLD3.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_RT_TLD3.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      this.tableLayoutPanel_RT_TLD3.Size = new Size(332, 131);
      this.tableLayoutPanel_RT_TLD3.TabIndex = 1;
      this.tableLayoutPanel_RT_TLD3_Check.ColumnCount = 2;
      this.tableLayoutPanel_RT_TLD3_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel_RT_TLD3_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
      this.tableLayoutPanel_RT_TLD3_Check.Controls.Add((Control) this.panel_RT_TLD3_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD3_Check.Controls.Add((Control) this.panel_RT_TLD3_UserName, 1, 0);
      this.tableLayoutPanel_RT_TLD3_Check.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD3_Check.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD3_Check.Name = "tableLayoutPanel_RT_TLD3_Check";
      this.tableLayoutPanel_RT_TLD3_Check.RowCount = 1;
      this.tableLayoutPanel_RT_TLD3_Check.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD3_Check.Size = new Size(332, 52);
      this.tableLayoutPanel_RT_TLD3_Check.TabIndex = 1;
      this.panel_RT_TLD3_Check.BackgroundImage = (Image) componentResourceManager.GetObject("panel_RT_TLD3_Check.BackgroundImage");
      this.panel_RT_TLD3_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel_RT_TLD3_Check.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD3_Check.Controls.Add((Control) this.pictureBox_RT_TLD3_Check);
      this.panel_RT_TLD3_Check.Location = new Point(0, 3);
      this.panel_RT_TLD3_Check.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD3_Check.Name = "panel_RT_TLD3_Check";
      this.panel_RT_TLD3_Check.Size = new Size(66, 49);
      this.panel_RT_TLD3_Check.TabIndex = 3;
      this.pictureBox_RT_TLD3_Check.BackColor = Color.Yellow;
      this.pictureBox_RT_TLD3_Check.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_TLD3_Check.BackgroundImage");
      this.pictureBox_RT_TLD3_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_TLD3_Check.Location = new Point(0, 0);
      this.pictureBox_RT_TLD3_Check.Name = "pictureBox_RT_TLD3_Check";
      this.pictureBox_RT_TLD3_Check.Size = new Size(46, 32);
      this.pictureBox_RT_TLD3_Check.TabIndex = 1;
      this.pictureBox_RT_TLD3_Check.TabStop = false;
      this.pictureBox_RT_TLD3_Check.Visible = false;
      this.panel_RT_TLD3_UserName.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD3_UserName.Controls.Add((Control) this.label_RT_TLD3_UserName);
      this.panel_RT_TLD3_UserName.Location = new Point(66, 3);
      this.panel_RT_TLD3_UserName.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD3_UserName.Name = "panel_RT_TLD3_UserName";
      this.panel_RT_TLD3_UserName.Size = new Size(266, 49);
      this.panel_RT_TLD3_UserName.TabIndex = 2;
      this.label_RT_TLD3_UserName.Font = new Font("Microsoft Sans Serif", 30f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD3_UserName.Location = new Point(-2, 2);
      this.label_RT_TLD3_UserName.Margin = new Padding(0);
      this.label_RT_TLD3_UserName.Name = "label_RT_TLD3_UserName";
      this.label_RT_TLD3_UserName.Size = new Size(183, 43);
      this.label_RT_TLD3_UserName.TabIndex = 0;
      this.label_RT_TLD3_UserName.Text = "User3";
      this.label_RT_TLD3_UserName.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_RT_TLD3_Value.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD3_Value.Controls.Add((Control) this.label_RT_TLD3_Value);
      this.panel_RT_TLD3_Value.Location = new Point(0, 52);
      this.panel_RT_TLD3_Value.Margin = new Padding(0);
      this.panel_RT_TLD3_Value.Name = "panel_RT_TLD3_Value";
      this.panel_RT_TLD3_Value.Size = new Size(332, 79);
      this.panel_RT_TLD3_Value.TabIndex = 2;
      this.label_RT_TLD3_Value.Font = new Font("Microsoft Sans Serif", 50.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD3_Value.Location = new Point(3, 4);
      this.label_RT_TLD3_Value.Margin = new Padding(0);
      this.label_RT_TLD3_Value.Name = "label_RT_TLD3_Value";
      this.label_RT_TLD3_Value.Size = new Size(271, 55);
      this.label_RT_TLD3_Value.TabIndex = 0;
      this.label_RT_TLD3_Value.Text = "Value";
      this.label_RT_TLD3_Value.TextAlign = ContentAlignment.MiddleCenter;
      this.tableLayoutPanel_RT_TLD2.ColumnCount = 1;
      this.tableLayoutPanel_RT_TLD2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD2.Controls.Add((Control) this.tableLayoutPanel_RT_TLD2_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD2.Controls.Add((Control) this.panel_RT_TLD2_Value, 0, 1);
      this.tableLayoutPanel_RT_TLD2.Location = new Point(331, 0);
      this.tableLayoutPanel_RT_TLD2.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD2.Name = "tableLayoutPanel_RT_TLD2";
      this.tableLayoutPanel_RT_TLD2.RowCount = 2;
      this.tableLayoutPanel_RT_TLD2.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_RT_TLD2.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      this.tableLayoutPanel_RT_TLD2.Size = new Size(330, 131);
      this.tableLayoutPanel_RT_TLD2.TabIndex = 1;
      this.tableLayoutPanel_RT_TLD2_Check.ColumnCount = 2;
      this.tableLayoutPanel_RT_TLD2_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel_RT_TLD2_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
      this.tableLayoutPanel_RT_TLD2_Check.Controls.Add((Control) this.panel_RT_TLD2_UserName, 1, 0);
      this.tableLayoutPanel_RT_TLD2_Check.Controls.Add((Control) this.panel_RT_TLD2_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD2_Check.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD2_Check.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD2_Check.Name = "tableLayoutPanel_RT_TLD2_Check";
      this.tableLayoutPanel_RT_TLD2_Check.RowCount = 1;
      this.tableLayoutPanel_RT_TLD2_Check.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD2_Check.Size = new Size(330, 52);
      this.tableLayoutPanel_RT_TLD2_Check.TabIndex = 1;
      this.panel_RT_TLD2_UserName.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD2_UserName.Controls.Add((Control) this.label_RT_TLD2_UserName);
      this.panel_RT_TLD2_UserName.Location = new Point(66, 3);
      this.panel_RT_TLD2_UserName.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD2_UserName.Name = "panel_RT_TLD2_UserName";
      this.panel_RT_TLD2_UserName.Size = new Size(264, 49);
      this.panel_RT_TLD2_UserName.TabIndex = 2;
      this.label_RT_TLD2_UserName.Font = new Font("Microsoft Sans Serif", 30f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD2_UserName.Location = new Point(1, 1);
      this.label_RT_TLD2_UserName.Margin = new Padding(0);
      this.label_RT_TLD2_UserName.Name = "label_RT_TLD2_UserName";
      this.label_RT_TLD2_UserName.Size = new Size(181, 44);
      this.label_RT_TLD2_UserName.TabIndex = 0;
      this.label_RT_TLD2_UserName.Text = "User2";
      this.label_RT_TLD2_UserName.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_RT_TLD2_Check.BackgroundImage = (Image) componentResourceManager.GetObject("panel_RT_TLD2_Check.BackgroundImage");
      this.panel_RT_TLD2_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel_RT_TLD2_Check.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD2_Check.Controls.Add((Control) this.pictureBox_RT_TLD2_Check);
      this.panel_RT_TLD2_Check.Location = new Point(0, 3);
      this.panel_RT_TLD2_Check.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD2_Check.Name = "panel_RT_TLD2_Check";
      this.panel_RT_TLD2_Check.Size = new Size(66, 49);
      this.panel_RT_TLD2_Check.TabIndex = 3;
      this.pictureBox_RT_TLD2_Check.BackColor = Color.Red;
      this.pictureBox_RT_TLD2_Check.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_TLD2_Check.BackgroundImage");
      this.pictureBox_RT_TLD2_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_TLD2_Check.Location = new Point(0, 0);
      this.pictureBox_RT_TLD2_Check.Name = "pictureBox_RT_TLD2_Check";
      this.pictureBox_RT_TLD2_Check.Size = new Size(44, 32);
      this.pictureBox_RT_TLD2_Check.TabIndex = 1;
      this.pictureBox_RT_TLD2_Check.TabStop = false;
      this.pictureBox_RT_TLD2_Check.Visible = false;
      this.panel_RT_TLD2_Value.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD2_Value.Controls.Add((Control) this.label_RT_TLD2_Value);
      this.panel_RT_TLD2_Value.Location = new Point(0, 52);
      this.panel_RT_TLD2_Value.Margin = new Padding(0);
      this.panel_RT_TLD2_Value.Name = "panel_RT_TLD2_Value";
      this.panel_RT_TLD2_Value.Size = new Size(330, 79);
      this.panel_RT_TLD2_Value.TabIndex = 2;
      this.label_RT_TLD2_Value.Font = new Font("Microsoft Sans Serif", 50.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD2_Value.Location = new Point(3, 2);
      this.label_RT_TLD2_Value.Margin = new Padding(0);
      this.label_RT_TLD2_Value.Name = "label_RT_TLD2_Value";
      this.label_RT_TLD2_Value.Size = new Size(271, 59);
      this.label_RT_TLD2_Value.TabIndex = 0;
      this.label_RT_TLD2_Value.Text = "Value";
      this.label_RT_TLD2_Value.TextAlign = ContentAlignment.MiddleCenter;
      this.tableLayoutPanel_RT_TLD1.ColumnCount = 1;
      this.tableLayoutPanel_RT_TLD1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD1.Controls.Add((Control) this.tableLayoutPanel_RT_TLD1_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD1.Controls.Add((Control) this.panel_RT_TLD1_Value, 0, 1);
      this.tableLayoutPanel_RT_TLD1.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD1.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD1.Name = "tableLayoutPanel_RT_TLD1";
      this.tableLayoutPanel_RT_TLD1.RowCount = 2;
      this.tableLayoutPanel_RT_TLD1.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_RT_TLD1.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      this.tableLayoutPanel_RT_TLD1.Size = new Size(330, 131);
      this.tableLayoutPanel_RT_TLD1.TabIndex = 1;
      this.tableLayoutPanel_RT_TLD1_Check.ColumnCount = 2;
      this.tableLayoutPanel_RT_TLD1_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel_RT_TLD1_Check.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
      this.tableLayoutPanel_RT_TLD1_Check.Controls.Add((Control) this.panel_RT_TLD1_UserName, 0, 0);
      this.tableLayoutPanel_RT_TLD1_Check.Controls.Add((Control) this.panel_RT_TLD1_Check, 0, 0);
      this.tableLayoutPanel_RT_TLD1_Check.Location = new Point(0, 0);
      this.tableLayoutPanel_RT_TLD1_Check.Margin = new Padding(0);
      this.tableLayoutPanel_RT_TLD1_Check.Name = "tableLayoutPanel_RT_TLD1_Check";
      this.tableLayoutPanel_RT_TLD1_Check.RowCount = 1;
      this.tableLayoutPanel_RT_TLD1_Check.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_RT_TLD1_Check.Size = new Size(330, 52);
      this.tableLayoutPanel_RT_TLD1_Check.TabIndex = 1;
      this.panel_RT_TLD1_UserName.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD1_UserName.Controls.Add((Control) this.label_RT_TLD1_UserName);
      this.panel_RT_TLD1_UserName.Location = new Point(66, 3);
      this.panel_RT_TLD1_UserName.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD1_UserName.Name = "panel_RT_TLD1_UserName";
      this.panel_RT_TLD1_UserName.Size = new Size(264, 49);
      this.panel_RT_TLD1_UserName.TabIndex = 4;
      this.label_RT_TLD1_UserName.Font = new Font("Microsoft Sans Serif", 30f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD1_UserName.Location = new Point(1, 1);
      this.label_RT_TLD1_UserName.Margin = new Padding(0);
      this.label_RT_TLD1_UserName.Name = "label_RT_TLD1_UserName";
      this.label_RT_TLD1_UserName.Size = new Size(179, 44);
      this.label_RT_TLD1_UserName.TabIndex = 0;
      this.label_RT_TLD1_UserName.Text = "User1";
      this.label_RT_TLD1_UserName.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_RT_TLD1_Check.BackgroundImage = (Image) componentResourceManager.GetObject("panel_RT_TLD1_Check.BackgroundImage");
      this.panel_RT_TLD1_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel_RT_TLD1_Check.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD1_Check.Controls.Add((Control) this.pictureBox_RT_TLD1_Check);
      this.panel_RT_TLD1_Check.Location = new Point(0, 3);
      this.panel_RT_TLD1_Check.Margin = new Padding(0, 3, 0, 0);
      this.panel_RT_TLD1_Check.Name = "panel_RT_TLD1_Check";
      this.panel_RT_TLD1_Check.Size = new Size(66, 49);
      this.panel_RT_TLD1_Check.TabIndex = 3;
      this.pictureBox_RT_TLD1_Check.BackColor = Color.Red;
      this.pictureBox_RT_TLD1_Check.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_RT_TLD1_Check.BackgroundImage");
      this.pictureBox_RT_TLD1_Check.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_RT_TLD1_Check.Location = new Point(-2, -2);
      this.pictureBox_RT_TLD1_Check.Name = "pictureBox_RT_TLD1_Check";
      this.pictureBox_RT_TLD1_Check.Size = new Size(47, 34);
      this.pictureBox_RT_TLD1_Check.TabIndex = 1;
      this.pictureBox_RT_TLD1_Check.TabStop = false;
      this.pictureBox_RT_TLD1_Check.Visible = false;
      this.panel_RT_TLD1_Value.BorderStyle = BorderStyle.Fixed3D;
      this.panel_RT_TLD1_Value.Controls.Add((Control) this.label_RT_TLD1_Value);
      this.panel_RT_TLD1_Value.Location = new Point(0, 52);
      this.panel_RT_TLD1_Value.Margin = new Padding(0);
      this.panel_RT_TLD1_Value.Name = "panel_RT_TLD1_Value";
      this.panel_RT_TLD1_Value.Size = new Size(330, 79);
      this.panel_RT_TLD1_Value.TabIndex = 2;
      this.label_RT_TLD1_Value.BackColor = Color.FromArgb(224, 224, 224);
      this.label_RT_TLD1_Value.Font = new Font("Microsoft Sans Serif", 50.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_RT_TLD1_Value.Location = new Point(3, 4);
      this.label_RT_TLD1_Value.Margin = new Padding(0);
      this.label_RT_TLD1_Value.Name = "label_RT_TLD1_Value";
      this.label_RT_TLD1_Value.Size = new Size(276, 57);
      this.label_RT_TLD1_Value.TabIndex = 0;
      this.label_RT_TLD1_Value.Text = "Value";
      this.label_RT_TLD1_Value.TextAlign = ContentAlignment.MiddleCenter;
      this.c1DockingTabPage_Settings.BackColor = Color.FromArgb(224, 224, 224);
      this.c1DockingTabPage_Settings.Controls.Add((Control) this.tableLayoutPanel_Config_Main);
      this.c1DockingTabPage_Settings.Location = new Point(44, 1);
      this.c1DockingTabPage_Settings.Name = "c1DockingTabPage_Settings";
      this.c1DockingTabPage_Settings.Size = new Size(994, 525);
      this.c1DockingTabPage_Settings.TabIndex = 1;
      this.c1DockingTabPage_Settings.Text = "RRPD Info";
      this.tableLayoutPanel_Config_Main.ColumnCount = 1;
      this.tableLayoutPanel_Config_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel_Config_Main.Controls.Add((Control) this.tableLayoutPanel_Config_Under, 0, 1);
      this.tableLayoutPanel_Config_Main.Controls.Add((Control) this.tableLayoutPanel_Config_UserTitle, 0, 0);
      this.tableLayoutPanel_Config_Main.Location = new Point(3, 3);
      this.tableLayoutPanel_Config_Main.Name = "tableLayoutPanel_Config_Main";
      this.tableLayoutPanel_Config_Main.RowCount = 2;
      this.tableLayoutPanel_Config_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel_Config_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 146f));
      this.tableLayoutPanel_Config_Main.Size = new Size(988, 519);
      this.tableLayoutPanel_Config_Main.TabIndex = 2;
      this.tableLayoutPanel_Config_Under.ColumnCount = 2;
      this.tableLayoutPanel_Config_Under.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75f));
      this.tableLayoutPanel_Config_Under.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel_Config_Under.Controls.Add((Control) this.tableLayoutPanel_Config_WarningCriticalTitle, 0, 0);
      this.tableLayoutPanel_Config_Under.Location = new Point(3, 376);
      this.tableLayoutPanel_Config_Under.Name = "tableLayoutPanel_Config_Under";
      this.tableLayoutPanel_Config_Under.RowCount = 1;
      this.tableLayoutPanel_Config_Under.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_Config_Under.Size = new Size(982, 140);
      this.tableLayoutPanel_Config_Under.TabIndex = 3;
      this.tableLayoutPanel_Config_WarningCriticalTitle.ColumnCount = 6;
      this.tableLayoutPanel_Config_WarningCriticalTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.0345f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.678164f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.35632f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.03449f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.70576f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.19077f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_criticalDay, 2, 2);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_Critical_Name, 0, 2);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_Warning_Name, 0, 1);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_criticalMonth, 3, 2);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_warningMonth, 3, 1);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_warningYear, 4, 1);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_criticalYear, 4, 2);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_warningTick, 5, 1);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_criticalTick, 5, 2);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.panel_Config_warningDay, 2, 1);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.label_Config_WarningCriticalTitle_Year, 4, 0);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.label_Config_WarningCriticalTitle_Month, 3, 0);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.label_Config_WarningCriticalTitle_Day, 2, 0);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Controls.Add((Control) this.tableLayoutPanel1, 5, 0);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Location = new Point(0, 0);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Margin = new Padding(0);
      this.tableLayoutPanel_Config_WarningCriticalTitle.Name = "tableLayoutPanel_Config_WarningCriticalTitle";
      this.tableLayoutPanel_Config_WarningCriticalTitle.RowCount = 3;
      this.tableLayoutPanel_Config_WarningCriticalTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.tableLayoutPanel_Config_WarningCriticalTitle.Size = new Size(736, 140);
      this.tableLayoutPanel_Config_WarningCriticalTitle.TabIndex = 2;
      this.panel_Config_criticalDay.BackColor = Color.FromArgb((int) byte.MaxValue, 192, 192);
      this.panel_Config_criticalDay.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_criticalDay.Controls.Add((Control) this.label_Config_criticalDay);
      this.panel_Config_criticalDay.Location = new Point(199, 87);
      this.panel_Config_criticalDay.Name = "panel_Config_criticalDay";
      this.panel_Config_criticalDay.Size = new Size(70, 49);
      this.panel_Config_criticalDay.TabIndex = 2;
      this.label_Config_criticalDay.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_criticalDay.Location = new Point(-3, 0);
      this.label_Config_criticalDay.Name = "label_Config_criticalDay";
      this.label_Config_criticalDay.Size = new Size(67, 45);
      this.label_Config_criticalDay.TabIndex = 2;
      this.label_Config_criticalDay.Text = "Value";
      this.label_Config_criticalDay.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_Critical_Name.BackColor = Color.FromArgb((int) byte.MaxValue, 32, 32);
      this.panel_Config_Critical_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_Critical_Name.Controls.Add((Control) this.label_Config_Critical_Name);
      this.panel_Config_Critical_Name.Location = new Point(3, 87);
      this.panel_Config_Critical_Name.Name = "panel_Config_Critical_Name";
      this.panel_Config_Critical_Name.Size = new Size(109, 49);
      this.panel_Config_Critical_Name.TabIndex = 6;
      this.label_Config_Critical_Name.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Critical_Name.Location = new Point(-2, 0);
      this.label_Config_Critical_Name.Name = "label_Config_Critical_Name";
      this.label_Config_Critical_Name.Size = new Size(118, 45);
      this.label_Config_Critical_Name.TabIndex = 2;
      this.label_Config_Critical_Name.Text = "CRITICAL";
      this.label_Config_Critical_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_Warning_Name.BackColor = Color.Yellow;
      this.panel_Config_Warning_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_Warning_Name.Controls.Add((Control) this.label_Config_Warning_Name);
      this.panel_Config_Warning_Name.Location = new Point(3, 31);
      this.panel_Config_Warning_Name.Name = "panel_Config_Warning_Name";
      this.panel_Config_Warning_Name.Size = new Size(109, 47);
      this.panel_Config_Warning_Name.TabIndex = 7;
      this.label_Config_Warning_Name.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Warning_Name.Location = new Point(-2, 0);
      this.label_Config_Warning_Name.Name = "label_Config_Warning_Name";
      this.label_Config_Warning_Name.Size = new Size(118, 43);
      this.label_Config_Warning_Name.TabIndex = 7;
      this.label_Config_Warning_Name.Text = "WARNING";
      this.label_Config_Warning_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_criticalMonth.BackColor = Color.FromArgb((int) byte.MaxValue, 192, 192);
      this.panel_Config_criticalMonth.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_criticalMonth.Controls.Add((Control) this.label_Config_criticalMonth);
      this.panel_Config_criticalMonth.Location = new Point(297, 87);
      this.panel_Config_criticalMonth.Name = "panel_Config_criticalMonth";
      this.panel_Config_criticalMonth.Size = new Size(109, 49);
      this.panel_Config_criticalMonth.TabIndex = 10;
      this.label_Config_criticalMonth.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_criticalMonth.Location = new Point(3, 0);
      this.label_Config_criticalMonth.Name = "label_Config_criticalMonth";
      this.label_Config_criticalMonth.Size = new Size(109, 45);
      this.label_Config_criticalMonth.TabIndex = 0;
      this.label_Config_criticalMonth.Text = "Value";
      this.label_Config_criticalMonth.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_warningMonth.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
      this.panel_Config_warningMonth.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_warningMonth.Controls.Add((Control) this.label_Config_warningMonth);
      this.panel_Config_warningMonth.Location = new Point(297, 31);
      this.panel_Config_warningMonth.Name = "panel_Config_warningMonth";
      this.panel_Config_warningMonth.Size = new Size(109, 47);
      this.panel_Config_warningMonth.TabIndex = 11;
      this.label_Config_warningMonth.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_warningMonth.Location = new Point(3, 2);
      this.label_Config_warningMonth.Name = "label_Config_warningMonth";
      this.label_Config_warningMonth.Size = new Size(109, 41);
      this.label_Config_warningMonth.TabIndex = 0;
      this.label_Config_warningMonth.Text = "Value";
      this.label_Config_warningMonth.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_warningYear.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
      this.panel_Config_warningYear.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_warningYear.Controls.Add((Control) this.label_Config_warningYear);
      this.panel_Config_warningYear.Location = new Point(444, 31);
      this.panel_Config_warningYear.Name = "panel_Config_warningYear";
      this.panel_Config_warningYear.Size = new Size(124, 47);
      this.panel_Config_warningYear.TabIndex = 14;
      this.label_Config_warningYear.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_warningYear.Location = new Point(3, 0);
      this.label_Config_warningYear.Name = "label_Config_warningYear";
      this.label_Config_warningYear.Size = new Size(134, 45);
      this.label_Config_warningYear.TabIndex = 0;
      this.label_Config_warningYear.Text = "Value";
      this.label_Config_warningYear.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_criticalYear.BackColor = Color.FromArgb((int) byte.MaxValue, 192, 192);
      this.panel_Config_criticalYear.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_criticalYear.Controls.Add((Control) this.label_Config_criticalYear);
      this.panel_Config_criticalYear.Location = new Point(444, 87);
      this.panel_Config_criticalYear.Name = "panel_Config_criticalYear";
      this.panel_Config_criticalYear.Size = new Size(124, 49);
      this.panel_Config_criticalYear.TabIndex = 15;
      this.label_Config_criticalYear.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_criticalYear.Location = new Point(1, 0);
      this.label_Config_criticalYear.Name = "label_Config_criticalYear";
      this.label_Config_criticalYear.Size = new Size(136, 45);
      this.label_Config_criticalYear.TabIndex = 0;
      this.label_Config_criticalYear.Text = "Value";
      this.label_Config_criticalYear.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_warningTick.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
      this.panel_Config_warningTick.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_warningTick.Controls.Add((Control) this.label_Config_warningTick);
      this.panel_Config_warningTick.Location = new Point(611, 31);
      this.panel_Config_warningTick.Name = "panel_Config_warningTick";
      this.panel_Config_warningTick.Size = new Size(73, 47);
      this.panel_Config_warningTick.TabIndex = 17;
      this.label_Config_warningTick.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_warningTick.Location = new Point(1, 0);
      this.label_Config_warningTick.Name = "label_Config_warningTick";
      this.label_Config_warningTick.Size = new Size(47, 43);
      this.label_Config_warningTick.TabIndex = 0;
      this.label_Config_warningTick.Text = "Value";
      this.label_Config_warningTick.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_criticalTick.BackColor = Color.FromArgb((int) byte.MaxValue, 192, 192);
      this.panel_Config_criticalTick.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_criticalTick.Controls.Add((Control) this.label_Config_criticalTick);
      this.panel_Config_criticalTick.Location = new Point(611, 87);
      this.panel_Config_criticalTick.Name = "panel_Config_criticalTick";
      this.panel_Config_criticalTick.Size = new Size(73, 49);
      this.panel_Config_criticalTick.TabIndex = 18;
      this.label_Config_criticalTick.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_criticalTick.Location = new Point(1, 0);
      this.label_Config_criticalTick.Name = "label_Config_criticalTick";
      this.label_Config_criticalTick.Size = new Size(47, 45);
      this.label_Config_criticalTick.TabIndex = 0;
      this.label_Config_criticalTick.Text = "Value";
      this.label_Config_criticalTick.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_warningDay.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
      this.panel_Config_warningDay.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_warningDay.Controls.Add((Control) this.label_Config_warningDay);
      this.panel_Config_warningDay.Location = new Point(199, 31);
      this.panel_Config_warningDay.Name = "panel_Config_warningDay";
      this.panel_Config_warningDay.Size = new Size(70, 47);
      this.panel_Config_warningDay.TabIndex = 20;
      this.label_Config_warningDay.Font = new Font("Arial", 20.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label_Config_warningDay.Location = new Point(-2, 2);
      this.label_Config_warningDay.Name = "label_Config_warningDay";
      this.label_Config_warningDay.Size = new Size(66, 41);
      this.label_Config_warningDay.TabIndex = 9;
      this.label_Config_warningDay.Text = "Value";
      this.label_Config_warningDay.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_WarningCriticalTitle_Year.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_WarningCriticalTitle_Year.Location = new Point(444, 0);
      this.label_Config_WarningCriticalTitle_Year.Name = "label_Config_WarningCriticalTitle_Year";
      this.label_Config_WarningCriticalTitle_Year.Size = new Size(124, 26);
      this.label_Config_WarningCriticalTitle_Year.TabIndex = 0;
      this.label_Config_WarningCriticalTitle_Year.Text = "Year";
      this.label_Config_WarningCriticalTitle_Year.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_WarningCriticalTitle_Month.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_WarningCriticalTitle_Month.Location = new Point(297, 0);
      this.label_Config_WarningCriticalTitle_Month.Name = "label_Config_WarningCriticalTitle_Month";
      this.label_Config_WarningCriticalTitle_Month.Size = new Size(109, 26);
      this.label_Config_WarningCriticalTitle_Month.TabIndex = 0;
      this.label_Config_WarningCriticalTitle_Month.Text = "Month";
      this.label_Config_WarningCriticalTitle_Month.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_WarningCriticalTitle_Day.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_WarningCriticalTitle_Day.Location = new Point(199, 0);
      this.label_Config_WarningCriticalTitle_Day.Name = "label_Config_WarningCriticalTitle_Day";
      this.label_Config_WarningCriticalTitle_Day.Size = new Size(70, 26);
      this.label_Config_WarningCriticalTitle_Day.TabIndex = 8;
      this.label_Config_WarningCriticalTitle_Day.Text = "Day";
      this.label_Config_WarningCriticalTitle_Day.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_WarningCriticalTitle_Tick.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_WarningCriticalTitle_Tick.Location = new Point(3, 0);
      this.label_Config_WarningCriticalTitle_Tick.Margin = new Padding(3, 0, 0, 0);
      this.label_Config_WarningCriticalTitle_Tick.Name = "label_Config_WarningCriticalTitle_Tick";
      this.label_Config_WarningCriticalTitle_Tick.Size = new Size(59, 21);
      this.label_Config_WarningCriticalTitle_Tick.TabIndex = 0;
      this.label_Config_WarningCriticalTitle_Tick.Text = "Tick";
      this.label_Config_WarningCriticalTitle_Tick.TextAlign = ContentAlignment.MiddleRight;
      this.tableLayoutPanel_Config_UserTitle.ColumnCount = 7;
      this.tableLayoutPanel_Config_UserTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5f));
      this.tableLayoutPanel_Config_UserTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel_Config_UserTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17f));
      this.tableLayoutPanel_Config_UserTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28f));
      this.tableLayoutPanel_Config_UserTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.pictureBox_user6_timer, 1, 6);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User4_Name, 0, 4);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User4_day, 2, 4);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User4_month, 3, 4);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User4_year, 4, 4);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User4_BTAddr, 5, 4);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User4_COM, 6, 4);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User5_Name, 0, 5);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User5_day, 2, 5);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User5_month, 3, 5);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User5_year, 4, 5);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User5_BTAddr, 5, 5);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User5_COM, 6, 5);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User6_Name, 0, 6);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User6_day, 2, 6);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User6_month, 3, 6);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User6_year, 4, 6);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User6_BTAddr, 5, 6);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User6_COM, 6, 6);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User3_COM, 6, 3);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User3_Name, 0, 3);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User3_BTAddr, 5, 3);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.pictureBox_user2_timer, 1, 2);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User3_year, 4, 3);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.pictureBox_user1_timer, 1, 1);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User3_month, 3, 3);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User2_COM, 6, 2);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User3_day, 2, 3);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User2_BTAddr, 5, 2);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User2_Name, 0, 2);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User1_COM, 6, 1);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.pictureBox_user3_timer, 1, 3);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User2_year, 4, 2);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User1_Name, 0, 1);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User2_month, 3, 2);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User1_BTAddr, 5, 1);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User2_day, 2, 2);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User1_year, 4, 1);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User1_month, 3, 1);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.panel_Config_User1_day, 2, 1);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.label_Config_Title_Name, 0, 0);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.label_Config_Title_day, 2, 0);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.label_Config_Title_month, 3, 0);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.label_Config_Title_COM, 6, 0);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.label_Config_Title_BTAddr, 5, 0);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.label_Config_Title_year, 4, 0);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.pictureBox_user4_timer, 1, 4);
      this.tableLayoutPanel_Config_UserTitle.Controls.Add((Control) this.pictureBox_user5_timer, 1, 5);
      this.tableLayoutPanel_Config_UserTitle.Location = new Point(3, 3);
      this.tableLayoutPanel_Config_UserTitle.Name = "tableLayoutPanel_Config_UserTitle";
      this.tableLayoutPanel_Config_UserTitle.RowCount = 7;
      this.tableLayoutPanel_Config_UserTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel_Config_UserTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
      this.tableLayoutPanel_Config_UserTitle.Size = new Size(982, 367);
      this.tableLayoutPanel_Config_UserTitle.TabIndex = 1;
      this.pictureBox_user6_timer.Location = new Point(150, 314);
      this.pictureBox_user6_timer.Name = "pictureBox_user6_timer";
      this.pictureBox_user6_timer.Size = new Size(33, 26);
      this.pictureBox_user6_timer.TabIndex = 34;
      this.pictureBox_user6_timer.TabStop = false;
      this.panel_Config_User4_Name.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User4_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User4_Name.Controls.Add((Control) this.label_Config_User4_Name);
      this.panel_Config_User4_Name.Location = new Point(3, 204);
      this.panel_Config_User4_Name.Name = "panel_Config_User4_Name";
      this.panel_Config_User4_Name.Size = new Size(112, 23);
      this.panel_Config_User4_Name.TabIndex = 14;
      this.label_Config_User4_Name.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User4_Name.Location = new Point(0, 0);
      this.label_Config_User4_Name.Name = "label_Config_User4_Name";
      this.label_Config_User4_Name.Size = new Size(119, 44);
      this.label_Config_User4_Name.TabIndex = 0;
      this.label_Config_User4_Name.Text = "User4";
      this.label_Config_User4_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User4_day.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User4_day.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User4_day.Controls.Add((Control) this.label_Config_User4_day);
      this.panel_Config_User4_day.Location = new Point(199, 204);
      this.panel_Config_User4_day.Name = "panel_Config_User4_day";
      this.panel_Config_User4_day.Size = new Size(73, 23);
      this.panel_Config_User4_day.TabIndex = 29;
      this.label_Config_User4_day.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User4_day.Location = new Point(-2, 0);
      this.label_Config_User4_day.Name = "label_Config_User4_day";
      this.label_Config_User4_day.Size = new Size(74, 44);
      this.label_Config_User4_day.TabIndex = 0;
      this.label_Config_User4_day.Text = "Value";
      this.label_Config_User4_day.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User4_month.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User4_month.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User4_month.Controls.Add((Control) this.label_Config_User4_month);
      this.panel_Config_User4_month.Location = new Point(297, 204);
      this.panel_Config_User4_month.Name = "panel_Config_User4_month";
      this.panel_Config_User4_month.Size = new Size(112, 23);
      this.panel_Config_User4_month.TabIndex = 28;
      this.label_Config_User4_month.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User4_month.Location = new Point(-2, 0);
      this.label_Config_User4_month.Name = "label_Config_User4_month";
      this.label_Config_User4_month.Size = new Size(117, 46);
      this.label_Config_User4_month.TabIndex = 0;
      this.label_Config_User4_month.Text = "Value";
      this.label_Config_User4_month.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User4_year.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User4_year.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User4_year.Controls.Add((Control) this.label_Config_User4_year);
      this.panel_Config_User4_year.Location = new Point(444, 204);
      this.panel_Config_User4_year.Name = "panel_Config_User4_year";
      this.panel_Config_User4_year.Size = new Size(128, 23);
      this.panel_Config_User4_year.TabIndex = 27;
      this.label_Config_User4_year.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User4_year.Location = new Point(-2, 0);
      this.label_Config_User4_year.Name = "label_Config_User4_year";
      this.label_Config_User4_year.Size = new Size(120, 44);
      this.label_Config_User4_year.TabIndex = 0;
      this.label_Config_User4_year.Text = "Value";
      this.label_Config_User4_year.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User4_BTAddr.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User4_BTAddr.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User4_BTAddr.Controls.Add((Control) this.label_Config_User4_BTAddr);
      this.panel_Config_User4_BTAddr.Location = new Point(610, 204);
      this.panel_Config_User4_BTAddr.Name = "panel_Config_User4_BTAddr";
      this.panel_Config_User4_BTAddr.Size = new Size(215, 23);
      this.panel_Config_User4_BTAddr.TabIndex = 26;
      this.label_Config_User4_BTAddr.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User4_BTAddr.Location = new Point(-2, -2);
      this.label_Config_User4_BTAddr.Name = "label_Config_User4_BTAddr";
      this.label_Config_User4_BTAddr.Size = new Size(235, 44);
      this.label_Config_User4_BTAddr.TabIndex = 0;
      this.label_Config_User4_BTAddr.Text = "Value";
      this.label_Config_User4_BTAddr.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User4_COM.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User4_COM.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User4_COM.Controls.Add((Control) this.label_Config_User4_COM);
      this.panel_Config_User4_COM.Location = new Point(884, 204);
      this.panel_Config_User4_COM.Name = "panel_Config_User4_COM";
      this.panel_Config_User4_COM.Size = new Size(75, 23);
      this.panel_Config_User4_COM.TabIndex = 25;
      this.label_Config_User4_COM.Font = new Font("Arial", 15.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User4_COM.Location = new Point(0, 0);
      this.label_Config_User4_COM.Name = "label_Config_User4_COM";
      this.label_Config_User4_COM.Size = new Size(66, 45);
      this.label_Config_User4_COM.TabIndex = 0;
      this.label_Config_User4_COM.Text = "Value";
      this.label_Config_User4_COM.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User5_Name.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User5_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User5_Name.Controls.Add((Control) this.label_Config_User5_Name);
      this.panel_Config_User5_Name.Location = new Point(3, 259);
      this.panel_Config_User5_Name.Name = "panel_Config_User5_Name";
      this.panel_Config_User5_Name.Size = new Size(112, 23);
      this.panel_Config_User5_Name.TabIndex = 24;
      this.label_Config_User5_Name.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User5_Name.Location = new Point(-2, -1);
      this.label_Config_User5_Name.Name = "label_Config_User5_Name";
      this.label_Config_User5_Name.Size = new Size(121, 41);
      this.label_Config_User5_Name.TabIndex = 0;
      this.label_Config_User5_Name.Text = "User5";
      this.label_Config_User5_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User5_day.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User5_day.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User5_day.Controls.Add((Control) this.label_Config_User5_day);
      this.panel_Config_User5_day.Location = new Point(199, 259);
      this.panel_Config_User5_day.Name = "panel_Config_User5_day";
      this.panel_Config_User5_day.Size = new Size(73, 23);
      this.panel_Config_User5_day.TabIndex = 23;
      this.label_Config_User5_day.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User5_day.Location = new Point(-2, 0);
      this.label_Config_User5_day.Name = "label_Config_User5_day";
      this.label_Config_User5_day.Size = new Size(74, 45);
      this.label_Config_User5_day.TabIndex = 0;
      this.label_Config_User5_day.Text = "Value";
      this.label_Config_User5_day.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User5_month.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User5_month.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User5_month.Controls.Add((Control) this.label_Config_User5_month);
      this.panel_Config_User5_month.Location = new Point(297, 259);
      this.panel_Config_User5_month.Name = "panel_Config_User5_month";
      this.panel_Config_User5_month.Size = new Size(112, 23);
      this.panel_Config_User5_month.TabIndex = 22;
      this.label_Config_User5_month.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User5_month.Location = new Point(3, 0);
      this.label_Config_User5_month.Name = "label_Config_User5_month";
      this.label_Config_User5_month.Size = new Size(108, 47);
      this.label_Config_User5_month.TabIndex = 0;
      this.label_Config_User5_month.Text = "Value";
      this.label_Config_User5_month.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User5_year.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User5_year.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User5_year.Controls.Add((Control) this.label_Config_User5_year);
      this.panel_Config_User5_year.Location = new Point(444, 259);
      this.panel_Config_User5_year.Name = "panel_Config_User5_year";
      this.panel_Config_User5_year.Size = new Size(128, 23);
      this.panel_Config_User5_year.TabIndex = 21;
      this.label_Config_User5_year.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User5_year.Location = new Point(0, 0);
      this.label_Config_User5_year.Name = "label_Config_User5_year";
      this.label_Config_User5_year.Size = new Size(118, 45);
      this.label_Config_User5_year.TabIndex = 0;
      this.label_Config_User5_year.Text = "Value";
      this.label_Config_User5_year.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User5_BTAddr.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User5_BTAddr.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User5_BTAddr.Controls.Add((Control) this.label_Config_User5_BTAddr);
      this.panel_Config_User5_BTAddr.Location = new Point(610, 259);
      this.panel_Config_User5_BTAddr.Name = "panel_Config_User5_BTAddr";
      this.panel_Config_User5_BTAddr.Size = new Size(215, 23);
      this.panel_Config_User5_BTAddr.TabIndex = 20;
      this.label_Config_User5_BTAddr.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User5_BTAddr.Location = new Point(0, 0);
      this.label_Config_User5_BTAddr.Name = "label_Config_User5_BTAddr";
      this.label_Config_User5_BTAddr.Size = new Size(233, 45);
      this.label_Config_User5_BTAddr.TabIndex = 0;
      this.label_Config_User5_BTAddr.Text = "Value";
      this.label_Config_User5_BTAddr.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User5_COM.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User5_COM.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User5_COM.Controls.Add((Control) this.label_Config_User5_COM);
      this.panel_Config_User5_COM.Location = new Point(884, 259);
      this.panel_Config_User5_COM.Name = "panel_Config_User5_COM";
      this.panel_Config_User5_COM.Size = new Size(75, 23);
      this.panel_Config_User5_COM.TabIndex = 19;
      this.label_Config_User5_COM.Font = new Font("Arial", 15.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User5_COM.Location = new Point(1, 0);
      this.label_Config_User5_COM.Name = "label_Config_User5_COM";
      this.label_Config_User5_COM.Size = new Size(65, 45);
      this.label_Config_User5_COM.TabIndex = 0;
      this.label_Config_User5_COM.Text = "Value";
      this.label_Config_User5_COM.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User6_Name.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User6_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User6_Name.Controls.Add((Control) this.label_Config_User6_Name);
      this.panel_Config_User6_Name.Location = new Point(3, 314);
      this.panel_Config_User6_Name.Name = "panel_Config_User6_Name";
      this.panel_Config_User6_Name.Size = new Size(112, 26);
      this.panel_Config_User6_Name.TabIndex = 18;
      this.label_Config_User6_Name.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User6_Name.Location = new Point(-2, 0);
      this.label_Config_User6_Name.Name = "label_Config_User6_Name";
      this.label_Config_User6_Name.Size = new Size(121, 45);
      this.label_Config_User6_Name.TabIndex = 0;
      this.label_Config_User6_Name.Text = "User6";
      this.label_Config_User6_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User6_day.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User6_day.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User6_day.Controls.Add((Control) this.label_Config_User6_day);
      this.panel_Config_User6_day.Location = new Point(199, 314);
      this.panel_Config_User6_day.Name = "panel_Config_User6_day";
      this.panel_Config_User6_day.Size = new Size(73, 26);
      this.panel_Config_User6_day.TabIndex = 17;
      this.label_Config_User6_day.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User6_day.Location = new Point(-2, 0);
      this.label_Config_User6_day.Name = "label_Config_User6_day";
      this.label_Config_User6_day.Size = new Size(74, 45);
      this.label_Config_User6_day.TabIndex = 0;
      this.label_Config_User6_day.Text = "Value";
      this.label_Config_User6_day.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User6_month.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User6_month.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User6_month.Controls.Add((Control) this.label_Config_User6_month);
      this.panel_Config_User6_month.Location = new Point(297, 314);
      this.panel_Config_User6_month.Name = "panel_Config_User6_month";
      this.panel_Config_User6_month.Size = new Size(112, 26);
      this.panel_Config_User6_month.TabIndex = 16;
      this.label_Config_User6_month.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User6_month.Location = new Point(-3, 0);
      this.label_Config_User6_month.Name = "label_Config_User6_month";
      this.label_Config_User6_month.Size = new Size(114, 45);
      this.label_Config_User6_month.TabIndex = 0;
      this.label_Config_User6_month.Text = "Value";
      this.label_Config_User6_month.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User6_year.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User6_year.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User6_year.Controls.Add((Control) this.label_Config_User6_year);
      this.panel_Config_User6_year.Location = new Point(444, 314);
      this.panel_Config_User6_year.Name = "panel_Config_User6_year";
      this.panel_Config_User6_year.Size = new Size(128, 26);
      this.panel_Config_User6_year.TabIndex = 15;
      this.label_Config_User6_year.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User6_year.Location = new Point(-2, 0);
      this.label_Config_User6_year.Name = "label_Config_User6_year";
      this.label_Config_User6_year.Size = new Size(120, 45);
      this.label_Config_User6_year.TabIndex = 0;
      this.label_Config_User6_year.Text = "Value";
      this.label_Config_User6_year.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User6_BTAddr.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User6_BTAddr.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User6_BTAddr.Controls.Add((Control) this.label_Config_User6_BTAddr);
      this.panel_Config_User6_BTAddr.Location = new Point(610, 314);
      this.panel_Config_User6_BTAddr.Name = "panel_Config_User6_BTAddr";
      this.panel_Config_User6_BTAddr.Size = new Size(215, 26);
      this.panel_Config_User6_BTAddr.TabIndex = 30;
      this.label_Config_User6_BTAddr.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User6_BTAddr.Location = new Point(-2, 0);
      this.label_Config_User6_BTAddr.Name = "label_Config_User6_BTAddr";
      this.label_Config_User6_BTAddr.Size = new Size(235, 45);
      this.label_Config_User6_BTAddr.TabIndex = 0;
      this.label_Config_User6_BTAddr.Text = "Value";
      this.label_Config_User6_BTAddr.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User6_COM.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User6_COM.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User6_COM.Controls.Add((Control) this.label_Config_User6_COM);
      this.panel_Config_User6_COM.Location = new Point(884, 314);
      this.panel_Config_User6_COM.Name = "panel_Config_User6_COM";
      this.panel_Config_User6_COM.Size = new Size(75, 26);
      this.panel_Config_User6_COM.TabIndex = 31;
      this.label_Config_User6_COM.Font = new Font("Arial", 15.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User6_COM.Location = new Point(-2, 0);
      this.label_Config_User6_COM.Name = "label_Config_User6_COM";
      this.label_Config_User6_COM.Size = new Size(66, 45);
      this.label_Config_User6_COM.TabIndex = 0;
      this.label_Config_User6_COM.Text = "Value";
      this.label_Config_User6_COM.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User3_COM.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User3_COM.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User3_COM.Controls.Add((Control) this.label_Config_User3_COM);
      this.panel_Config_User3_COM.Location = new Point(884, 149);
      this.panel_Config_User3_COM.Name = "panel_Config_User3_COM";
      this.panel_Config_User3_COM.Size = new Size(75, 23);
      this.panel_Config_User3_COM.TabIndex = 2;
      this.label_Config_User3_COM.Font = new Font("Arial", 15.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User3_COM.Location = new Point(0, 0);
      this.label_Config_User3_COM.Name = "label_Config_User3_COM";
      this.label_Config_User3_COM.Size = new Size(66, 44);
      this.label_Config_User3_COM.TabIndex = 0;
      this.label_Config_User3_COM.Text = "Value";
      this.label_Config_User3_COM.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User3_Name.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User3_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User3_Name.Controls.Add((Control) this.label_Config_User3_Name);
      this.panel_Config_User3_Name.Location = new Point(3, 149);
      this.panel_Config_User3_Name.Name = "panel_Config_User3_Name";
      this.panel_Config_User3_Name.Size = new Size(112, 23);
      this.panel_Config_User3_Name.TabIndex = 2;
      this.label_Config_User3_Name.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User3_Name.Location = new Point(1, -2);
      this.label_Config_User3_Name.Name = "label_Config_User3_Name";
      this.label_Config_User3_Name.Size = new Size(118, 48);
      this.label_Config_User3_Name.TabIndex = 0;
      this.label_Config_User3_Name.Text = "User3";
      this.label_Config_User3_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User3_BTAddr.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User3_BTAddr.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User3_BTAddr.Controls.Add((Control) this.label_Config_User3_BTAddr);
      this.panel_Config_User3_BTAddr.Location = new Point(610, 149);
      this.panel_Config_User3_BTAddr.Name = "panel_Config_User3_BTAddr";
      this.panel_Config_User3_BTAddr.Size = new Size(215, 23);
      this.panel_Config_User3_BTAddr.TabIndex = 2;
      this.label_Config_User3_BTAddr.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User3_BTAddr.Location = new Point(-2, 0);
      this.label_Config_User3_BTAddr.Name = "label_Config_User3_BTAddr";
      this.label_Config_User3_BTAddr.Size = new Size(235, 46);
      this.label_Config_User3_BTAddr.TabIndex = 0;
      this.label_Config_User3_BTAddr.Text = "Value";
      this.label_Config_User3_BTAddr.TextAlign = ContentAlignment.MiddleCenter;
      this.pictureBox_user2_timer.Location = new Point(150, 94);
      this.pictureBox_user2_timer.Name = "pictureBox_user2_timer";
      this.pictureBox_user2_timer.Size = new Size(33, 23);
      this.pictureBox_user2_timer.TabIndex = 4;
      this.pictureBox_user2_timer.TabStop = false;
      this.panel_Config_User3_year.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User3_year.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User3_year.Controls.Add((Control) this.label_Config_User3_year);
      this.panel_Config_User3_year.Location = new Point(444, 149);
      this.panel_Config_User3_year.Name = "panel_Config_User3_year";
      this.panel_Config_User3_year.Size = new Size(128, 23);
      this.panel_Config_User3_year.TabIndex = 2;
      this.label_Config_User3_year.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User3_year.Location = new Point(-2, 0);
      this.label_Config_User3_year.Name = "label_Config_User3_year";
      this.label_Config_User3_year.Size = new Size(120, 44);
      this.label_Config_User3_year.TabIndex = 0;
      this.label_Config_User3_year.Text = "Value";
      this.label_Config_User3_year.TextAlign = ContentAlignment.MiddleCenter;
      this.pictureBox_user1_timer.Location = new Point(150, 39);
      this.pictureBox_user1_timer.Name = "pictureBox_user1_timer";
      this.pictureBox_user1_timer.Size = new Size(33, 23);
      this.pictureBox_user1_timer.TabIndex = 3;
      this.pictureBox_user1_timer.TabStop = false;
      this.panel_Config_User3_month.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User3_month.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User3_month.Controls.Add((Control) this.label_Config_User3_month);
      this.panel_Config_User3_month.Location = new Point(297, 149);
      this.panel_Config_User3_month.Name = "panel_Config_User3_month";
      this.panel_Config_User3_month.Size = new Size(112, 23);
      this.panel_Config_User3_month.TabIndex = 2;
      this.label_Config_User3_month.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User3_month.Location = new Point(-2, -2);
      this.label_Config_User3_month.Name = "label_Config_User3_month";
      this.label_Config_User3_month.Size = new Size(117, 44);
      this.label_Config_User3_month.TabIndex = 0;
      this.label_Config_User3_month.Text = "Value";
      this.label_Config_User3_month.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User2_COM.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User2_COM.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User2_COM.Controls.Add((Control) this.label_Config_User2_COM);
      this.panel_Config_User2_COM.Location = new Point(884, 94);
      this.panel_Config_User2_COM.Name = "panel_Config_User2_COM";
      this.panel_Config_User2_COM.Size = new Size(75, 23);
      this.panel_Config_User2_COM.TabIndex = 2;
      this.label_Config_User2_COM.Font = new Font("Arial", 15.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User2_COM.Location = new Point(0, 0);
      this.label_Config_User2_COM.Name = "label_Config_User2_COM";
      this.label_Config_User2_COM.Size = new Size(66, 45);
      this.label_Config_User2_COM.TabIndex = 0;
      this.label_Config_User2_COM.Text = "Value";
      this.label_Config_User2_COM.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User3_day.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User3_day.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User3_day.Controls.Add((Control) this.label_Config_User3_day);
      this.panel_Config_User3_day.Location = new Point(199, 149);
      this.panel_Config_User3_day.Name = "panel_Config_User3_day";
      this.panel_Config_User3_day.Size = new Size(73, 23);
      this.panel_Config_User3_day.TabIndex = 2;
      this.label_Config_User3_day.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User3_day.Location = new Point(-2, -1);
      this.label_Config_User3_day.Name = "label_Config_User3_day";
      this.label_Config_User3_day.Size = new Size(74, 41);
      this.label_Config_User3_day.TabIndex = 0;
      this.label_Config_User3_day.Text = "Value";
      this.label_Config_User3_day.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User2_BTAddr.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User2_BTAddr.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User2_BTAddr.Controls.Add((Control) this.label_Config_User2_BTAddr);
      this.panel_Config_User2_BTAddr.Location = new Point(610, 94);
      this.panel_Config_User2_BTAddr.Name = "panel_Config_User2_BTAddr";
      this.panel_Config_User2_BTAddr.Size = new Size(215, 23);
      this.panel_Config_User2_BTAddr.TabIndex = 2;
      this.label_Config_User2_BTAddr.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User2_BTAddr.Location = new Point(-2, 0);
      this.label_Config_User2_BTAddr.Name = "label_Config_User2_BTAddr";
      this.label_Config_User2_BTAddr.Size = new Size(235, 45);
      this.label_Config_User2_BTAddr.TabIndex = 0;
      this.label_Config_User2_BTAddr.Text = "Value";
      this.label_Config_User2_BTAddr.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User2_Name.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User2_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User2_Name.Controls.Add((Control) this.label_Config_User2_Name);
      this.panel_Config_User2_Name.Location = new Point(3, 94);
      this.panel_Config_User2_Name.Name = "panel_Config_User2_Name";
      this.panel_Config_User2_Name.Size = new Size(112, 23);
      this.panel_Config_User2_Name.TabIndex = 2;
      this.label_Config_User2_Name.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User2_Name.Location = new Point(1, 0);
      this.label_Config_User2_Name.Name = "label_Config_User2_Name";
      this.label_Config_User2_Name.Size = new Size(118, 47);
      this.label_Config_User2_Name.TabIndex = 0;
      this.label_Config_User2_Name.Text = "User2";
      this.label_Config_User2_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User1_COM.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User1_COM.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User1_COM.Controls.Add((Control) this.label_Config_User1_COM);
      this.panel_Config_User1_COM.Location = new Point(884, 39);
      this.panel_Config_User1_COM.Name = "panel_Config_User1_COM";
      this.panel_Config_User1_COM.Size = new Size(75, 23);
      this.panel_Config_User1_COM.TabIndex = 2;
      this.label_Config_User1_COM.Font = new Font("Arial", 15.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User1_COM.Location = new Point(0, 0);
      this.label_Config_User1_COM.Name = "label_Config_User1_COM";
      this.label_Config_User1_COM.Size = new Size(64, 45);
      this.label_Config_User1_COM.TabIndex = 0;
      this.label_Config_User1_COM.Text = "Value";
      this.label_Config_User1_COM.TextAlign = ContentAlignment.MiddleCenter;
      this.pictureBox_user3_timer.Location = new Point(150, 149);
      this.pictureBox_user3_timer.Name = "pictureBox_user3_timer";
      this.pictureBox_user3_timer.Size = new Size(33, 23);
      this.pictureBox_user3_timer.TabIndex = 4;
      this.pictureBox_user3_timer.TabStop = false;
      this.panel_Config_User2_year.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User2_year.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User2_year.Controls.Add((Control) this.label_Config_User2_year);
      this.panel_Config_User2_year.Location = new Point(444, 94);
      this.panel_Config_User2_year.Name = "panel_Config_User2_year";
      this.panel_Config_User2_year.Size = new Size(128, 23);
      this.panel_Config_User2_year.TabIndex = 2;
      this.label_Config_User2_year.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User2_year.Location = new Point(0, 0);
      this.label_Config_User2_year.Name = "label_Config_User2_year";
      this.label_Config_User2_year.Size = new Size(118, 45);
      this.label_Config_User2_year.TabIndex = 0;
      this.label_Config_User2_year.Text = "Value";
      this.label_Config_User2_year.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User1_Name.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User1_Name.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User1_Name.Controls.Add((Control) this.label_Config_User1_Name);
      this.panel_Config_User1_Name.Location = new Point(3, 39);
      this.panel_Config_User1_Name.Name = "panel_Config_User1_Name";
      this.panel_Config_User1_Name.Size = new Size(112, 23);
      this.panel_Config_User1_Name.TabIndex = 2;
      this.label_Config_User1_Name.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User1_Name.Location = new Point(1, 0);
      this.label_Config_User1_Name.Name = "label_Config_User1_Name";
      this.label_Config_User1_Name.Size = new Size(118, 45);
      this.label_Config_User1_Name.TabIndex = 0;
      this.label_Config_User1_Name.Text = "User1";
      this.label_Config_User1_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User2_month.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User2_month.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User2_month.Controls.Add((Control) this.label_Config_User2_month);
      this.panel_Config_User2_month.Location = new Point(297, 94);
      this.panel_Config_User2_month.Name = "panel_Config_User2_month";
      this.panel_Config_User2_month.Size = new Size(112, 23);
      this.panel_Config_User2_month.TabIndex = 2;
      this.label_Config_User2_month.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User2_month.Location = new Point(-2, 0);
      this.label_Config_User2_month.Name = "label_Config_User2_month";
      this.label_Config_User2_month.Size = new Size(117, 45);
      this.label_Config_User2_month.TabIndex = 0;
      this.label_Config_User2_month.Text = "Value";
      this.label_Config_User2_month.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User1_BTAddr.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User1_BTAddr.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User1_BTAddr.Controls.Add((Control) this.label_Config_User1_BTAddr);
      this.panel_Config_User1_BTAddr.Location = new Point(610, 39);
      this.panel_Config_User1_BTAddr.Name = "panel_Config_User1_BTAddr";
      this.panel_Config_User1_BTAddr.Size = new Size(215, 23);
      this.panel_Config_User1_BTAddr.TabIndex = 2;
      this.label_Config_User1_BTAddr.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User1_BTAddr.Location = new Point(-2, 0);
      this.label_Config_User1_BTAddr.Name = "label_Config_User1_BTAddr";
      this.label_Config_User1_BTAddr.Size = new Size(235, 45);
      this.label_Config_User1_BTAddr.TabIndex = 0;
      this.label_Config_User1_BTAddr.Text = "Value";
      this.label_Config_User1_BTAddr.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User2_day.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User2_day.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User2_day.Controls.Add((Control) this.label_Config_User2_day);
      this.panel_Config_User2_day.Location = new Point(199, 94);
      this.panel_Config_User2_day.Name = "panel_Config_User2_day";
      this.panel_Config_User2_day.Size = new Size(73, 23);
      this.panel_Config_User2_day.TabIndex = 2;
      this.label_Config_User2_day.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User2_day.Location = new Point(-3, 0);
      this.label_Config_User2_day.Name = "label_Config_User2_day";
      this.label_Config_User2_day.Size = new Size(75, 45);
      this.label_Config_User2_day.TabIndex = 0;
      this.label_Config_User2_day.Text = "Value";
      this.label_Config_User2_day.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User1_year.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User1_year.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User1_year.Controls.Add((Control) this.label_Config_User1_year);
      this.panel_Config_User1_year.Location = new Point(444, 39);
      this.panel_Config_User1_year.Name = "panel_Config_User1_year";
      this.panel_Config_User1_year.Size = new Size(128, 23);
      this.panel_Config_User1_year.TabIndex = 2;
      this.label_Config_User1_year.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User1_year.Location = new Point(-2, 0);
      this.label_Config_User1_year.Name = "label_Config_User1_year";
      this.label_Config_User1_year.Size = new Size(120, 45);
      this.label_Config_User1_year.TabIndex = 0;
      this.label_Config_User1_year.Text = "Value";
      this.label_Config_User1_year.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User1_month.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User1_month.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User1_month.Controls.Add((Control) this.label_Config_User1_month);
      this.panel_Config_User1_month.Location = new Point(297, 39);
      this.panel_Config_User1_month.Name = "panel_Config_User1_month";
      this.panel_Config_User1_month.Size = new Size(112, 23);
      this.panel_Config_User1_month.TabIndex = 2;
      this.label_Config_User1_month.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User1_month.Location = new Point(-2, 0);
      this.label_Config_User1_month.Name = "label_Config_User1_month";
      this.label_Config_User1_month.Size = new Size(117, 45);
      this.label_Config_User1_month.TabIndex = 0;
      this.label_Config_User1_month.Text = "Value";
      this.label_Config_User1_month.TextAlign = ContentAlignment.MiddleCenter;
      this.panel_Config_User1_day.BackColor = Color.FromArgb(224, 224, 224);
      this.panel_Config_User1_day.BorderStyle = BorderStyle.Fixed3D;
      this.panel_Config_User1_day.Controls.Add((Control) this.label_Config_User1_day);
      this.panel_Config_User1_day.Location = new Point(199, 39);
      this.panel_Config_User1_day.Name = "panel_Config_User1_day";
      this.panel_Config_User1_day.Size = new Size(73, 23);
      this.panel_Config_User1_day.TabIndex = 2;
      this.label_Config_User1_day.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label_Config_User1_day.Location = new Point(-2, 0);
      this.label_Config_User1_day.Name = "label_Config_User1_day";
      this.label_Config_User1_day.Size = new Size(74, 45);
      this.label_Config_User1_day.TabIndex = 0;
      this.label_Config_User1_day.Text = "Value";
      this.label_Config_User1_day.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_Title_Name.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Title_Name.Location = new Point(3, 0);
      this.label_Config_Title_Name.Name = "label_Config_Title_Name";
      this.label_Config_Title_Name.Size = new Size(112, 19);
      this.label_Config_Title_Name.TabIndex = 3;
      this.label_Config_Title_Name.Text = "UserName";
      this.label_Config_Title_Name.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_Title_day.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Title_day.Location = new Point(199, 0);
      this.label_Config_Title_day.Name = "label_Config_Title_day";
      this.label_Config_Title_day.Size = new Size(73, 19);
      this.label_Config_Title_day.TabIndex = 5;
      this.label_Config_Title_day.Text = "Day";
      this.label_Config_Title_day.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_Title_month.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Title_month.Location = new Point(297, 0);
      this.label_Config_Title_month.Name = "label_Config_Title_month";
      this.label_Config_Title_month.Size = new Size(112, 19);
      this.label_Config_Title_month.TabIndex = 6;
      this.label_Config_Title_month.Text = "Month";
      this.label_Config_Title_month.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_Title_COM.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Title_COM.Location = new Point(884, 0);
      this.label_Config_Title_COM.Name = "label_Config_Title_COM";
      this.label_Config_Title_COM.Size = new Size(75, 19);
      this.label_Config_Title_COM.TabIndex = 9;
      this.label_Config_Title_COM.Text = "Port";
      this.label_Config_Title_COM.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_Title_BTAddr.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Title_BTAddr.Location = new Point(610, 0);
      this.label_Config_Title_BTAddr.Name = "label_Config_Title_BTAddr";
      this.label_Config_Title_BTAddr.Size = new Size(215, 19);
      this.label_Config_Title_BTAddr.TabIndex = 8;
      this.label_Config_Title_BTAddr.Text = "BT Addr";
      this.label_Config_Title_BTAddr.TextAlign = ContentAlignment.MiddleCenter;
      this.label_Config_Title_year.Font = new Font("Arial", 15.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label_Config_Title_year.Location = new Point(444, 0);
      this.label_Config_Title_year.Name = "label_Config_Title_year";
      this.label_Config_Title_year.Size = new Size(128, 19);
      this.label_Config_Title_year.TabIndex = 7;
      this.label_Config_Title_year.Text = "Year";
      this.label_Config_Title_year.TextAlign = ContentAlignment.MiddleCenter;
      this.pictureBox_user4_timer.Location = new Point(150, 204);
      this.pictureBox_user4_timer.Name = "pictureBox_user4_timer";
      this.pictureBox_user4_timer.Size = new Size(33, 23);
      this.pictureBox_user4_timer.TabIndex = 32;
      this.pictureBox_user4_timer.TabStop = false;
      this.pictureBox_user5_timer.Location = new Point(150, 259);
      this.pictureBox_user5_timer.Name = "pictureBox_user5_timer";
      this.pictureBox_user5_timer.Size = new Size(33, 23);
      this.pictureBox_user5_timer.TabIndex = 33;
      this.pictureBox_user5_timer.TabStop = false;
      this.tableLayoutPanel_TopMenu.BackColor = SystemColors.ControlDarkDark;
      this.tableLayoutPanel_TopMenu.ColumnCount = 3;
      this.tableLayoutPanel_TopMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.72727f));
      this.tableLayoutPanel_TopMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.18182f));
      this.tableLayoutPanel_TopMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.090908f));
      this.tableLayoutPanel_TopMenu.Controls.Add((Control) this.pictureBox_CompanyLogo, 0, 0);
      this.tableLayoutPanel_TopMenu.Controls.Add((Control) this.pictureBox_Exit, 2, 0);
      this.tableLayoutPanel_TopMenu.Controls.Add((Control) this.pictureBox_ProductName, 1, 0);
      this.tableLayoutPanel_TopMenu.Location = new Point(0, 0);
      this.tableLayoutPanel_TopMenu.Margin = new Padding(0);
      this.tableLayoutPanel_TopMenu.Name = "tableLayoutPanel_TopMenu";
      this.tableLayoutPanel_TopMenu.RowCount = 1;
      this.tableLayoutPanel_TopMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel_TopMenu.Size = new Size(769, 73);
      this.tableLayoutPanel_TopMenu.TabIndex = 0;
      this.pictureBox_CompanyLogo.BackColor = Color.FromArgb(244, 244, 244);
      this.pictureBox_CompanyLogo.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_CompanyLogo.BackgroundImage");
      this.pictureBox_CompanyLogo.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_CompanyLogo.BorderStyle = BorderStyle.Fixed3D;
      this.pictureBox_CompanyLogo.Location = new Point(0, 0);
      this.pictureBox_CompanyLogo.Margin = new Padding(0);
      this.pictureBox_CompanyLogo.Name = "pictureBox_CompanyLogo";
      this.pictureBox_CompanyLogo.Size = new Size(174, 73);
      this.pictureBox_CompanyLogo.TabIndex = 0;
      this.pictureBox_CompanyLogo.TabStop = false;
      this.pictureBox_Exit.BackColor = SystemColors.ControlDarkDark;
      this.pictureBox_Exit.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_Exit.BackgroundImage");
      this.pictureBox_Exit.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_Exit.BorderStyle = BorderStyle.Fixed3D;
      this.pictureBox_Exit.Location = new Point(698, 0);
      this.pictureBox_Exit.Margin = new Padding(0);
      this.pictureBox_Exit.Name = "pictureBox_Exit";
      this.pictureBox_Exit.Size = new Size(70, 73);
      this.pictureBox_Exit.TabIndex = 2;
      this.pictureBox_Exit.TabStop = false;
      this.pictureBox_Exit.Click += new EventHandler(this.pictureBox_Exit_Click);
      this.pictureBox_ProductName.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox_ProductName.BackgroundImage");
      this.pictureBox_ProductName.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureBox_ProductName.BorderStyle = BorderStyle.Fixed3D;
      this.pictureBox_ProductName.Location = new Point(174, 0);
      this.pictureBox_ProductName.Margin = new Padding(0);
      this.pictureBox_ProductName.Name = "pictureBox_ProductName";
      this.pictureBox_ProductName.Size = new Size(524, 73);
      this.pictureBox_ProductName.TabIndex = 3;
      this.pictureBox_ProductName.TabStop = false;
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.label_Config_WarningCriticalTitle_Tick, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.label1, 1, 0);
      this.tableLayoutPanel1.Location = new Point(612, 4);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel1.Size = new Size(124, 21);
      this.tableLayoutPanel1.TabIndex = 21;
      this.label1.Font = new Font("Arial", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(62, 0);
      this.label1.Margin = new Padding(0, 0, 3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(28, 16);
      this.label1.TabIndex = 3;
      this.label1.Text = "(CPS)";
      this.label1.TextAlign = ContentAlignment.BottomLeft;
      this.AutoScaleDimensions = new SizeF(7f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(224, 224, 224);
      this.ClientSize = new Size(1060, 648);
      this.Controls.Add((Control) this.tableLayoutPanel_Main);
      this.Name = nameof (Main);
      this.Text = "Realtime Personal Dosimeter";
      this.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
      this.Load += new EventHandler(this.Main_Load);
      this.Move += new EventHandler(this.Main_Move);
      this.Resize += new EventHandler(this.Main_Resize);
      this.tableLayoutPanel_Main.ResumeLayout(false);
      ((ISupportInitialize) this.c1DockingTab_Main).EndInit();
      this.c1DockingTab_Main.ResumeLayout(false);
      this.c1DockingTabPage_RT.ResumeLayout(false);
      this.tableLayoutPanel_RT_Main.ResumeLayout(false);
      this.tableLayoutPanel_RT_Head2.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD6.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD6_Check.ResumeLayout(false);
      this.panel_RT_TLD6_Check.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_RT_TLD6_Check).EndInit();
      this.panel_RT_TLD6_UserName.ResumeLayout(false);
      this.panel_RT_TLD6_Value.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD4.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD4_Check.ResumeLayout(false);
      this.panel_RT_TLD4_Check.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_RT_TLD4_Check).EndInit();
      this.panel_RT_TLD4_UserName.ResumeLayout(false);
      this.panel_RT_TLD4_Value.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD5.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD5_Check.ResumeLayout(false);
      this.panel_RT_TLD5_UserName.ResumeLayout(false);
      this.panel_RT_TLD5_Check.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_RT_TLD5_Check).EndInit();
      this.panel_RT_TLD5_Value.ResumeLayout(false);
      this.panel_RT_Chart.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_RT_Chart_Time).EndInit();
      ((ISupportInitialize) this.pictureBox_RT_Chart_uSv).EndInit();
      ((ISupportInitialize) this.pictureBox_RT_Chart_CPS).EndInit();
      this.chart_RT_CPSChart.EndInit();
      this.tableLayoutPanel_RT_Head1.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD3.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD3_Check.ResumeLayout(false);
      this.panel_RT_TLD3_Check.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_RT_TLD3_Check).EndInit();
      this.panel_RT_TLD3_UserName.ResumeLayout(false);
      this.panel_RT_TLD3_Value.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD2.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD2_Check.ResumeLayout(false);
      this.panel_RT_TLD2_UserName.ResumeLayout(false);
      this.panel_RT_TLD2_Check.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_RT_TLD2_Check).EndInit();
      this.panel_RT_TLD2_Value.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD1.ResumeLayout(false);
      this.tableLayoutPanel_RT_TLD1_Check.ResumeLayout(false);
      this.panel_RT_TLD1_UserName.ResumeLayout(false);
      this.panel_RT_TLD1_Check.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_RT_TLD1_Check).EndInit();
      this.panel_RT_TLD1_Value.ResumeLayout(false);
      this.c1DockingTabPage_Settings.ResumeLayout(false);
      this.tableLayoutPanel_Config_Main.ResumeLayout(false);
      this.tableLayoutPanel_Config_Under.ResumeLayout(false);
      this.tableLayoutPanel_Config_WarningCriticalTitle.ResumeLayout(false);
      this.panel_Config_criticalDay.ResumeLayout(false);
      this.panel_Config_Critical_Name.ResumeLayout(false);
      this.panel_Config_Warning_Name.ResumeLayout(false);
      this.panel_Config_criticalMonth.ResumeLayout(false);
      this.panel_Config_warningMonth.ResumeLayout(false);
      this.panel_Config_warningYear.ResumeLayout(false);
      this.panel_Config_criticalYear.ResumeLayout(false);
      this.panel_Config_warningTick.ResumeLayout(false);
      this.panel_Config_criticalTick.ResumeLayout(false);
      this.panel_Config_warningDay.ResumeLayout(false);
      this.tableLayoutPanel_Config_UserTitle.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_user6_timer).EndInit();
      this.panel_Config_User4_Name.ResumeLayout(false);
      this.panel_Config_User4_day.ResumeLayout(false);
      this.panel_Config_User4_month.ResumeLayout(false);
      this.panel_Config_User4_year.ResumeLayout(false);
      this.panel_Config_User4_BTAddr.ResumeLayout(false);
      this.panel_Config_User4_COM.ResumeLayout(false);
      this.panel_Config_User5_Name.ResumeLayout(false);
      this.panel_Config_User5_day.ResumeLayout(false);
      this.panel_Config_User5_month.ResumeLayout(false);
      this.panel_Config_User5_year.ResumeLayout(false);
      this.panel_Config_User5_BTAddr.ResumeLayout(false);
      this.panel_Config_User5_COM.ResumeLayout(false);
      this.panel_Config_User6_Name.ResumeLayout(false);
      this.panel_Config_User6_day.ResumeLayout(false);
      this.panel_Config_User6_month.ResumeLayout(false);
      this.panel_Config_User6_year.ResumeLayout(false);
      this.panel_Config_User6_BTAddr.ResumeLayout(false);
      this.panel_Config_User6_COM.ResumeLayout(false);
      this.panel_Config_User3_COM.ResumeLayout(false);
      this.panel_Config_User3_Name.ResumeLayout(false);
      this.panel_Config_User3_BTAddr.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_user2_timer).EndInit();
      this.panel_Config_User3_year.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_user1_timer).EndInit();
      this.panel_Config_User3_month.ResumeLayout(false);
      this.panel_Config_User2_COM.ResumeLayout(false);
      this.panel_Config_User3_day.ResumeLayout(false);
      this.panel_Config_User2_BTAddr.ResumeLayout(false);
      this.panel_Config_User2_Name.ResumeLayout(false);
      this.panel_Config_User1_COM.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_user3_timer).EndInit();
      this.panel_Config_User2_year.ResumeLayout(false);
      this.panel_Config_User1_Name.ResumeLayout(false);
      this.panel_Config_User2_month.ResumeLayout(false);
      this.panel_Config_User1_BTAddr.ResumeLayout(false);
      this.panel_Config_User2_day.ResumeLayout(false);
      this.panel_Config_User1_year.ResumeLayout(false);
      this.panel_Config_User1_month.ResumeLayout(false);
      this.panel_Config_User1_day.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_user4_timer).EndInit();
      ((ISupportInitialize) this.pictureBox_user5_timer).EndInit();
      this.tableLayoutPanel_TopMenu.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox_CompanyLogo).EndInit();
      ((ISupportInitialize) this.pictureBox_Exit).EndInit();
      ((ISupportInitialize) this.pictureBox_ProductName).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private enum BLE_STATE
    {
      STANDBY,
      OPENING,
      OPENED,
      SCANNING,
      SCANNED,
      CONNECTING,
      CONNECTED,
      FINDING_SERVICE,
      FOUND_SERVICE,
      REQUEST_HANDLE,
      RESPONSED_HANDLE,
      REQUEST_CPS,
      LISTENING_CPS,
    }
  }
}
