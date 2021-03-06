﻿
Imports System.Configuration.ConfigurationManager

Public Class Core_BL

    Public KO_ID As String = AppSettings("KO_ID").ToString
    Public ServerRoot As String = AppSettings("ServerRoot").ToString

    Public Dispense1_Port As String = AppSettings("Dispense1_Port").ToString
    Public Dispense2_Port As String = AppSettings("Dispense2_Port").ToString
    Public Dispense5_Port As String = AppSettings("Dispense5_Port").ToString
    Public Dispense10_Port As String = AppSettings("Dispense10_Port").ToString
    Public Dispense20_Port As String = AppSettings("Dispense20_Port").ToString
    Public Dispense50_Port As String = AppSettings("Dispense50_Port").ToString
    Public Dispense100_Port As String = AppSettings("Dispense100_Port").ToString
    Public Dispense500_Port As String = AppSettings("Dispense500_Port").ToString
    Public Dispense1000_Port As String = AppSettings("Dispense1000_Port").ToString
    Public CashReciever_Port As String = AppSettings("CashReciever_Port").ToString
    Public CoinReciever_Port As String = AppSettings("CoinReciever_Port").ToString

    Public Product_Picker_IP As String = AppSettings("Product_Picker_IP").ToString
    Public Product_Picker_Port As Integer = AppSettings("Product_Picker_Port").ToString

    Public Simulate_Pick_Product As Boolean = CBool(AppSettings("Simulate_Pick_Product"))
    Public Simulate_Pick_SIM As Boolean = CBool(AppSettings("Simulate_Pick_SIM"))

    Public SIM_Dispenser_Port As String = AppSettings("SIM_Dispenser_Port").ToString
    Public IDCardReader As String = AppSettings("IDCardReader").ToString
    Public PassportScanPath As String = AppSettings("PassportScanPath").ToString


End Class
