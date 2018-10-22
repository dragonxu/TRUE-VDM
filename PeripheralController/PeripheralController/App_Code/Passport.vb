Public Class Passport
    Public FirstName As String = ""
    Public MiddleName As String = ""
    Public LastName As String = ""
    Public DocType As String = ""
    Public Nationality As String = ""
    Public PassportNo As String = ""
    Public DateOfBirth As String = ""
    Public Sex As String = ""
    Public Expire As String = ""
    Public PersonalID As String = ""
    Public IssueCountry As String = ""
    Public MRZ As String = ""
    Public Photo As String = ""

    Public Function MRZToCusInfo(ByVal MRZ As String) As Passport

        On Error Resume Next
        Dim C As New Converter
        Dim Result As New Passport
        Result.MRZ = MRZ

        Dim NamePart As String = MRZ.Substring(5, 39)
        Dim NameSplit As String() = NamePart.Split({"<"}, StringSplitOptions.RemoveEmptyEntries)
        If NameSplit.Length = 3 Then
            Result.FirstName = NameSplit(2)
            Result.MiddleName = NameSplit(1)
        Else
            Result.FirstName = NameSplit(1)
        End If
        Result.LastName = NameSplit(0)

        Dim OtherPart As String = MRZ.Substring(44)

        Result.DocType = MRZ.Substring(0, 1)
        Result.Nationality = OtherPart.Substring(10, 3)
        Result.PassportNo = OtherPart.Substring(0, 9).Replace("<", "")
        Dim DOB As String = C.StringToDate(OtherPart.Substring(13, 6), "yyMMdd").ToString("yyyy-MM-dd")
        Result.DateOfBirth = DOB.ToString
        Result.Sex = OtherPart.Substring(20, 1)
        Dim Expire As String = C.StringToDate(OtherPart.Substring(21, 6), "yyMMdd").ToString("yyyy-MM-dd")
        Result.Expire = Expire.ToString
        Result.PersonalID = OtherPart.Substring(28, 14).Replace("<", "")
        Result.IssueCountry = MRZ.Substring(2, 3)

        Return Result
    End Function

End Class
