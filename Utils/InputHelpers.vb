Option Strict On
Option Explicit On

Public Module InputHelpers

    Public Function NormalizeText(value As String) As String
        Return If(value, String.Empty).Trim()
    End Function

    Public Function HasText(value As String) As Boolean
        Return NormalizeText(value) <> String.Empty
    End Function

    Public Function BuildContainsSearchValue(value As String) As String
        Return $"%{NormalizeText(value)}%"
    End Function

    Public Function ParseULongOrDefault(value As Object, Optional defaultValue As ULong = 0UL) As ULong

        If value Is Nothing OrElse value Is DBNull.Value Then
            Return defaultValue
        End If

        Dim parsedValue As ULong
        If ULong.TryParse(value.ToString(), parsedValue) Then
            Return parsedValue
        End If

        Return defaultValue

    End Function

    Public Function ParseNullableULong(value As Object) As ULong?

        Dim parsedValue As ULong = ParseULongOrDefault(value, 0UL)
        If parsedValue = 0UL Then
            Return Nothing
        End If

        Return parsedValue

    End Function

    Public Function ToDbNullIfWhiteSpace(value As String) As Object

        Dim normalizedValue As String = NormalizeText(value)
        If normalizedValue = String.Empty Then
            Return DBNull.Value
        End If

        Return normalizedValue

    End Function

    Public Function ToDbNullUpper(value As String) As Object

        Dim normalizedValue As String = NormalizeText(value)
        If normalizedValue = String.Empty Then
            Return DBNull.Value
        End If

        Return normalizedValue.ToUpperInvariant()

    End Function

    Public Function ToDbNullLower(value As String) As Object

        Dim normalizedValue As String = NormalizeText(value)
        If normalizedValue = String.Empty Then
            Return DBNull.Value
        End If

        Return normalizedValue.ToLowerInvariant()

    End Function

    Public Function ToDbNull(value As Date?) As Object

        If value.HasValue Then
            Return value.Value
        End If

        Return DBNull.Value

    End Function

End Module
