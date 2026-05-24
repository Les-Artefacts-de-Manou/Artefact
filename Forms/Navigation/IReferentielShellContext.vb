Option Strict On
Option Explicit On

Public Interface IReferentielShellContext
    ReadOnly Property SharedToolTip As ToolTip
    ReadOnly Property SharedErrorProvider As ErrorProvider

    Sub SetStatus(message As String, Optional statusKind As UtilsForm.FormStatusKind = UtilsForm.FormStatusKind.Info)
    Sub SetContext(moduleName As String, mode As UtilsForm.ModeEdition)
    Sub NavigateHome()
End Interface
