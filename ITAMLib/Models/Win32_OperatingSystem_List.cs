﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ITAMLib.Models
{
  public class Win32_OperatingSystem_List
  {
    public string ComputerName { get; set; }
    public List<Win32_OperatingSystem> Items = new List<Win32_OperatingSystem>();

    public Win32_OperatingSystem_List(string WmiClass, string members)
    {
      ComputerName = System.Environment.MachineName;
      CollectWmiClass(WmiClass, members);
    }

    private void CollectWmiClass(string wmiClass, string members)
    {
      Items.Clear();

      try
      {
        foreach (ManagementObject managementObject in WmiList.GetCollection(wmiClass, members))
        {
          WmiRecord record = new WmiRecord(members);
          foreach (PropertyData propertyData in managementObject.Properties)
          {
            record.ProcessProperty(propertyData);
          }
          Items.Add(new Win32_OperatingSystem(record));
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Quering the WMI results in an exception:\n{ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
    }
  }
}
