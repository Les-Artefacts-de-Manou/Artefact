<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_Recommandations
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        tlpCenter = New TableLayoutPanel()
        pnlLeftOrigines = New Panel()
        grpOrigineDetails = New GroupBox()
        tlpOrigineDetails = New TableLayoutPanel()
        lblIdOrigine = New Label()
        txtIdOrigine = New TextBox()
        lblCodeOrigine = New Label()
        txtCodeOrigine = New TextBox()
        lblLibelleOrigine = New Label()
        txtLibelleOrigine = New TextBox()
        lblOrdreOrigine = New Label()
        nudOrdreOrigine = New NumericUpDown()
        chkOrigineActif = New CheckBox()
        pnlActionsOrigine = New Panel()
        btnNewOrigine = New Button()
        btnEditOrigine = New Button()
        btnSaveOrigine = New Button()
        btnCancelOrigine = New Button()
        btnDeleteOrigine = New Button()
        dgvOrigines = New DataGridView()
        pnlTopSearchOrigines = New Panel()
        lblCountOrigines = New Label()
        chkOriginesActifs = New CheckBox()
        btnClearSearchOrigines = New Button()
        btnSearchOrigines = New Button()
        txtSearchOrigines = New TextBox()
        lblSearchOrigines = New Label()
        pnlRightDetails = New Panel()
        grpRecommandations = New GroupBox()
        tlpRecommandations = New TableLayoutPanel()
        pnlTopSearchRecommandations = New Panel()
        lblCountRecommandations = New Label()
        chkSearchNotes = New CheckBox()
        chkRecommandationsActifs = New CheckBox()
        btnClearSearchRecommandations = New Button()
        btnSearchRecommandations = New Button()
        txtSearchRecommandations = New TextBox()
        lblSearchRecommandations = New Label()
        dgvRecommandations = New DataGridView()
        tlpRecommandationDetails = New TableLayoutPanel()
        lblIdRecommandation = New Label()
        txtIdRecommandation = New TextBox()
        lblCodeRecommandation = New Label()
        txtCodeRecommandation = New TextBox()
        lblSourceNom = New Label()
        txtSourceNom = New TextBox()
        lblSourceLogin = New Label()
        txtSourceLogin = New TextBox()
        lblSourceUrl = New Label()
        txtSourceUrl = New TextBox()
        lblDateRecommandation = New Label()
        dtpDateRecommandation = New DateTimePicker()
        chkRecommandationActive = New CheckBox()
        lblCommentaire = New Label()
        pnlRichTextContainer = New Panel()
        rtbCommentaire = New RichTextBox()
        ucRichTextToolbar = New UC_RichTextToolbar()
        pnlActionsRecommandation = New Panel()
        btnNewRecommandation = New Button()
        btnEditRecommandation = New Button()
        btnSaveRecommandation = New Button()
        btnCancelRecommandation = New Button()
        btnDeleteRecommandation = New Button()
        tlpCenter.SuspendLayout()
        pnlLeftOrigines.SuspendLayout()
        grpOrigineDetails.SuspendLayout()
        tlpOrigineDetails.SuspendLayout()
        CType(nudOrdreOrigine, ComponentModel.ISupportInitialize).BeginInit()
        pnlActionsOrigine.SuspendLayout()
        CType(dgvOrigines, ComponentModel.ISupportInitialize).BeginInit()
        pnlTopSearchOrigines.SuspendLayout()
        pnlRightDetails.SuspendLayout()
        grpRecommandations.SuspendLayout()
        tlpRecommandations.SuspendLayout()
        pnlTopSearchRecommandations.SuspendLayout()
        CType(dgvRecommandations, ComponentModel.ISupportInitialize).BeginInit()
        tlpRecommandationDetails.SuspendLayout()
        pnlRichTextContainer.SuspendLayout()
        pnlActionsRecommandation.SuspendLayout()
        SuspendLayout()
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 583F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenter.Controls.Add(pnlLeftOrigines, 0, 0)
        tlpCenter.Controls.Add(pnlRightDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(0, 0)
        tlpCenter.Margin = New Padding(4, 3, 4, 3)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(1198, 836)
        tlpCenter.TabIndex = 0
        ' 
        ' pnlLeftOrigines
        ' 
        pnlLeftOrigines.Controls.Add(grpOrigineDetails)
        pnlLeftOrigines.Controls.Add(dgvOrigines)
        pnlLeftOrigines.Controls.Add(pnlTopSearchOrigines)
        pnlLeftOrigines.Dock = DockStyle.Fill
        pnlLeftOrigines.Location = New Point(4, 3)
        pnlLeftOrigines.Margin = New Padding(4, 3, 4, 3)
        pnlLeftOrigines.Name = "pnlLeftOrigines"
        pnlLeftOrigines.Size = New Size(575, 830)
        pnlLeftOrigines.TabIndex = 0
        ' 
        ' grpOrigineDetails
        ' 
        grpOrigineDetails.Controls.Add(tlpOrigineDetails)
        grpOrigineDetails.Controls.Add(pnlActionsOrigine)
        grpOrigineDetails.Dock = DockStyle.Bottom
        grpOrigineDetails.Location = New Point(0, 622)
        grpOrigineDetails.Margin = New Padding(4, 3, 4, 3)
        grpOrigineDetails.Name = "grpOrigineDetails"
        grpOrigineDetails.Padding = New Padding(4, 3, 4, 3)
        grpOrigineDetails.Size = New Size(575, 208)
        grpOrigineDetails.TabIndex = 0
        grpOrigineDetails.TabStop = False
        grpOrigineDetails.Text = "Détails Origine"
        ' 
        ' tlpOrigineDetails
        ' 
        tlpOrigineDetails.ColumnCount = 2
        tlpOrigineDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140F))
        tlpOrigineDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpOrigineDetails.Controls.Add(lblIdOrigine, 0, 0)
        tlpOrigineDetails.Controls.Add(txtIdOrigine, 1, 0)
        tlpOrigineDetails.Controls.Add(lblCodeOrigine, 0, 1)
        tlpOrigineDetails.Controls.Add(txtCodeOrigine, 1, 1)
        tlpOrigineDetails.Controls.Add(lblLibelleOrigine, 0, 2)
        tlpOrigineDetails.Controls.Add(txtLibelleOrigine, 1, 2)
        tlpOrigineDetails.Controls.Add(lblOrdreOrigine, 0, 3)
        tlpOrigineDetails.Controls.Add(nudOrdreOrigine, 1, 3)
        tlpOrigineDetails.Controls.Add(chkOrigineActif, 1, 4)
        tlpOrigineDetails.Dock = DockStyle.Fill
        tlpOrigineDetails.Location = New Point(4, 19)
        tlpOrigineDetails.Margin = New Padding(4, 3, 4, 3)
        tlpOrigineDetails.Name = "tlpOrigineDetails"
        tlpOrigineDetails.RowCount = 5
        tlpOrigineDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpOrigineDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpOrigineDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpOrigineDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpOrigineDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpOrigineDetails.Size = New Size(567, 151)
        tlpOrigineDetails.TabIndex = 0
        ' 
        ' lblIdOrigine
        ' 
        lblIdOrigine.Anchor = AnchorStyles.Left
        lblIdOrigine.AutoSize = True
        lblIdOrigine.Location = New Point(4, 7)
        lblIdOrigine.Margin = New Padding(4, 0, 4, 0)
        lblIdOrigine.Name = "lblIdOrigine"
        lblIdOrigine.Size = New Size(18, 15)
        lblIdOrigine.TabIndex = 0
        lblIdOrigine.Text = "ID"
        ' 
        ' txtIdOrigine
        ' 
        txtIdOrigine.Anchor = AnchorStyles.Left
        txtIdOrigine.Location = New Point(144, 3)
        txtIdOrigine.Margin = New Padding(4, 3, 4, 3)
        txtIdOrigine.Name = "txtIdOrigine"
        txtIdOrigine.ReadOnly = True
        txtIdOrigine.Size = New Size(116, 23)
        txtIdOrigine.TabIndex = 1
        txtIdOrigine.TabStop = False
        ' 
        ' lblCodeOrigine
        ' 
        lblCodeOrigine.Anchor = AnchorStyles.Left
        lblCodeOrigine.AutoSize = True
        lblCodeOrigine.Location = New Point(4, 37)
        lblCodeOrigine.Margin = New Padding(4, 0, 4, 0)
        lblCodeOrigine.Name = "lblCodeOrigine"
        lblCodeOrigine.Size = New Size(35, 15)
        lblCodeOrigine.TabIndex = 2
        lblCodeOrigine.Text = "Code"
        ' 
        ' txtCodeOrigine
        ' 
        txtCodeOrigine.Anchor = AnchorStyles.Left
        txtCodeOrigine.Location = New Point(144, 33)
        txtCodeOrigine.Margin = New Padding(4, 3, 4, 3)
        txtCodeOrigine.Name = "txtCodeOrigine"
        txtCodeOrigine.ReadOnly = True
        txtCodeOrigine.Size = New Size(116, 23)
        txtCodeOrigine.TabIndex = 3
        txtCodeOrigine.TabStop = False
        ' 
        ' lblLibelleOrigine
        ' 
        lblLibelleOrigine.Anchor = AnchorStyles.Left
        lblLibelleOrigine.AutoSize = True
        lblLibelleOrigine.Location = New Point(4, 67)
        lblLibelleOrigine.Margin = New Padding(4, 0, 4, 0)
        lblLibelleOrigine.Name = "lblLibelleOrigine"
        lblLibelleOrigine.Size = New Size(41, 15)
        lblLibelleOrigine.TabIndex = 4
        lblLibelleOrigine.Text = "Libellé"
        ' 
        ' txtLibelleOrigine
        ' 
        txtLibelleOrigine.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtLibelleOrigine.Location = New Point(144, 63)
        txtLibelleOrigine.Margin = New Padding(4, 3, 4, 3)
        txtLibelleOrigine.Name = "txtLibelleOrigine"
        txtLibelleOrigine.Size = New Size(419, 23)
        txtLibelleOrigine.TabIndex = 5
        ' 
        ' lblOrdreOrigine
        ' 
        lblOrdreOrigine.Anchor = AnchorStyles.Left
        lblOrdreOrigine.AutoSize = True
        lblOrdreOrigine.Location = New Point(4, 97)
        lblOrdreOrigine.Margin = New Padding(4, 0, 4, 0)
        lblOrdreOrigine.Name = "lblOrdreOrigine"
        lblOrdreOrigine.Size = New Size(37, 15)
        lblOrdreOrigine.TabIndex = 6
        lblOrdreOrigine.Text = "Ordre"
        ' 
        ' nudOrdreOrigine
        ' 
        nudOrdreOrigine.Anchor = AnchorStyles.Left
        nudOrdreOrigine.Location = New Point(144, 93)
        nudOrdreOrigine.Margin = New Padding(4, 3, 4, 3)
        nudOrdreOrigine.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        nudOrdreOrigine.Name = "nudOrdreOrigine"
        nudOrdreOrigine.Size = New Size(93, 23)
        nudOrdreOrigine.TabIndex = 7
        ' 
        ' chkOrigineActif
        ' 
        chkOrigineActif.Anchor = AnchorStyles.Left
        chkOrigineActif.AutoSize = True
        chkOrigineActif.Location = New Point(144, 126)
        chkOrigineActif.Margin = New Padding(4, 3, 4, 3)
        chkOrigineActif.Name = "chkOrigineActif"
        chkOrigineActif.Size = New Size(51, 19)
        chkOrigineActif.TabIndex = 8
        chkOrigineActif.Text = "Actif"
        chkOrigineActif.UseVisualStyleBackColor = True
        ' 
        ' pnlActionsOrigine
        ' 
        pnlActionsOrigine.Controls.Add(btnNewOrigine)
        pnlActionsOrigine.Controls.Add(btnEditOrigine)
        pnlActionsOrigine.Controls.Add(btnSaveOrigine)
        pnlActionsOrigine.Controls.Add(btnCancelOrigine)
        pnlActionsOrigine.Controls.Add(btnDeleteOrigine)
        pnlActionsOrigine.Dock = DockStyle.Bottom
        pnlActionsOrigine.Location = New Point(4, 170)
        pnlActionsOrigine.Margin = New Padding(4, 3, 4, 3)
        pnlActionsOrigine.Name = "pnlActionsOrigine"
        pnlActionsOrigine.Size = New Size(567, 35)
        pnlActionsOrigine.TabIndex = 1
        ' 
        ' btnNewOrigine
        ' 
        btnNewOrigine.Location = New Point(4, 3)
        btnNewOrigine.Margin = New Padding(4, 3, 4, 3)
        btnNewOrigine.Name = "btnNewOrigine"
        btnNewOrigine.Size = New Size(88, 27)
        btnNewOrigine.TabIndex = 0
        btnNewOrigine.Text = "Nouveau"
        btnNewOrigine.UseVisualStyleBackColor = True
        ' 
        ' btnEditOrigine
        ' 
        btnEditOrigine.Location = New Point(98, 3)
        btnEditOrigine.Margin = New Padding(4, 3, 4, 3)
        btnEditOrigine.Name = "btnEditOrigine"
        btnEditOrigine.Size = New Size(88, 27)
        btnEditOrigine.TabIndex = 1
        btnEditOrigine.Text = "Modifier"
        btnEditOrigine.UseVisualStyleBackColor = True
        ' 
        ' btnSaveOrigine
        ' 
        btnSaveOrigine.Location = New Point(192, 3)
        btnSaveOrigine.Margin = New Padding(4, 3, 4, 3)
        btnSaveOrigine.Name = "btnSaveOrigine"
        btnSaveOrigine.Size = New Size(88, 27)
        btnSaveOrigine.TabIndex = 2
        btnSaveOrigine.Text = "Enregistrer"
        btnSaveOrigine.UseVisualStyleBackColor = True
        ' 
        ' btnCancelOrigine
        ' 
        btnCancelOrigine.Location = New Point(287, 3)
        btnCancelOrigine.Margin = New Padding(4, 3, 4, 3)
        btnCancelOrigine.Name = "btnCancelOrigine"
        btnCancelOrigine.Size = New Size(88, 27)
        btnCancelOrigine.TabIndex = 3
        btnCancelOrigine.Text = "Annuler"
        btnCancelOrigine.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteOrigine
        ' 
        btnDeleteOrigine.Location = New Point(382, 3)
        btnDeleteOrigine.Margin = New Padding(4, 3, 4, 3)
        btnDeleteOrigine.Name = "btnDeleteOrigine"
        btnDeleteOrigine.Size = New Size(88, 27)
        btnDeleteOrigine.TabIndex = 4
        btnDeleteOrigine.Text = "Supprimer"
        btnDeleteOrigine.UseVisualStyleBackColor = True
        ' 
        ' dgvOrigines
        ' 
        dgvOrigines.AllowUserToAddRows = False
        dgvOrigines.AllowUserToDeleteRows = False
        dgvOrigines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrigines.Dock = DockStyle.Fill
        dgvOrigines.Location = New Point(0, 69)
        dgvOrigines.Margin = New Padding(4, 3, 4, 3)
        dgvOrigines.MultiSelect = False
        dgvOrigines.Name = "dgvOrigines"
        dgvOrigines.ReadOnly = True
        dgvOrigines.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrigines.Size = New Size(575, 761)
        dgvOrigines.TabIndex = 1
        ' 
        ' pnlTopSearchOrigines
        ' 
        pnlTopSearchOrigines.Controls.Add(lblCountOrigines)
        pnlTopSearchOrigines.Controls.Add(chkOriginesActifs)
        pnlTopSearchOrigines.Controls.Add(btnClearSearchOrigines)
        pnlTopSearchOrigines.Controls.Add(btnSearchOrigines)
        pnlTopSearchOrigines.Controls.Add(txtSearchOrigines)
        pnlTopSearchOrigines.Controls.Add(lblSearchOrigines)
        pnlTopSearchOrigines.Dock = DockStyle.Top
        pnlTopSearchOrigines.Location = New Point(0, 0)
        pnlTopSearchOrigines.Margin = New Padding(4, 3, 4, 3)
        pnlTopSearchOrigines.Name = "pnlTopSearchOrigines"
        pnlTopSearchOrigines.Size = New Size(575, 69)
        pnlTopSearchOrigines.TabIndex = 0
        ' 
        ' lblCountOrigines
        ' 
        lblCountOrigines.AutoSize = True
        lblCountOrigines.Location = New Point(12, 46)
        lblCountOrigines.Margin = New Padding(4, 0, 4, 0)
        lblCountOrigines.Name = "lblCountOrigines"
        lblCountOrigines.Size = New Size(53, 15)
        lblCountOrigines.TabIndex = 5
        lblCountOrigines.Text = "0 origine"
        ' 
        ' chkOriginesActifs
        ' 
        chkOriginesActifs.AutoSize = True
        chkOriginesActifs.Location = New Point(408, 12)
        chkOriginesActifs.Margin = New Padding(4, 3, 4, 3)
        chkOriginesActifs.Name = "chkOriginesActifs"
        chkOriginesActifs.Size = New Size(85, 19)
        chkOriginesActifs.TabIndex = 4
        chkOriginesActifs.Text = "Actifs seuls"
        chkOriginesActifs.UseVisualStyleBackColor = True
        ' 
        ' btnClearSearchOrigines
        ' 
        btnClearSearchOrigines.Location = New Point(373, 9)
        btnClearSearchOrigines.Margin = New Padding(4, 3, 4, 3)
        btnClearSearchOrigines.Name = "btnClearSearchOrigines"
        btnClearSearchOrigines.Size = New Size(28, 27)
        btnClearSearchOrigines.TabIndex = 3
        btnClearSearchOrigines.Text = "✖"
        btnClearSearchOrigines.UseVisualStyleBackColor = True
        ' 
        ' btnSearchOrigines
        ' 
        btnSearchOrigines.Location = New Point(338, 9)
        btnSearchOrigines.Margin = New Padding(4, 3, 4, 3)
        btnSearchOrigines.Name = "btnSearchOrigines"
        btnSearchOrigines.Size = New Size(28, 27)
        btnSearchOrigines.TabIndex = 2
        btnSearchOrigines.Text = "🔍"
        btnSearchOrigines.UseVisualStyleBackColor = True
        ' 
        ' txtSearchOrigines
        ' 
        txtSearchOrigines.Location = New Point(88, 12)
        txtSearchOrigines.Margin = New Padding(4, 3, 4, 3)
        txtSearchOrigines.Name = "txtSearchOrigines"
        txtSearchOrigines.Size = New Size(243, 23)
        txtSearchOrigines.TabIndex = 1
        ' 
        ' lblSearchOrigines
        ' 
        lblSearchOrigines.AutoSize = True
        lblSearchOrigines.Location = New Point(12, 15)
        lblSearchOrigines.Margin = New Padding(4, 0, 4, 0)
        lblSearchOrigines.Name = "lblSearchOrigines"
        lblSearchOrigines.Size = New Size(62, 15)
        lblSearchOrigines.TabIndex = 0
        lblSearchOrigines.Text = "Recherche"
        ' 
        ' pnlRightDetails
        ' 
        pnlRightDetails.Controls.Add(grpRecommandations)
        pnlRightDetails.Dock = DockStyle.Fill
        pnlRightDetails.Location = New Point(587, 3)
        pnlRightDetails.Margin = New Padding(4, 3, 4, 3)
        pnlRightDetails.Name = "pnlRightDetails"
        pnlRightDetails.Size = New Size(607, 830)
        pnlRightDetails.TabIndex = 1
        ' 
        ' grpRecommandations
        ' 
        grpRecommandations.Controls.Add(tlpRecommandations)
        grpRecommandations.Dock = DockStyle.Fill
        grpRecommandations.Location = New Point(0, 0)
        grpRecommandations.Margin = New Padding(4, 3, 4, 3)
        grpRecommandations.Name = "grpRecommandations"
        grpRecommandations.Padding = New Padding(4, 3, 4, 3)
        grpRecommandations.Size = New Size(607, 830)
        grpRecommandations.TabIndex = 1
        grpRecommandations.TabStop = False
        grpRecommandations.Text = "Recommandations"
        ' 
        ' tlpRecommandations
        ' 
        tlpRecommandations.ColumnCount = 1
        tlpRecommandations.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpRecommandations.Controls.Add(pnlTopSearchRecommandations, 0, 0)
        tlpRecommandations.Controls.Add(dgvRecommandations, 0, 1)
        tlpRecommandations.Controls.Add(tlpRecommandationDetails, 0, 2)
        tlpRecommandations.Controls.Add(pnlActionsRecommandation, 0, 3)
        tlpRecommandations.Dock = DockStyle.Fill
        tlpRecommandations.Location = New Point(4, 19)
        tlpRecommandations.Margin = New Padding(4, 3, 4, 3)
        tlpRecommandations.Name = "tlpRecommandations"
        tlpRecommandations.RowCount = 4
        tlpRecommandations.RowStyles.Add(New RowStyle(SizeType.Absolute, 69F))
        tlpRecommandations.RowStyles.Add(New RowStyle(SizeType.Absolute, 208F))
        tlpRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpRecommandations.RowStyles.Add(New RowStyle(SizeType.Absolute, 35F))
        tlpRecommandations.Size = New Size(599, 808)
        tlpRecommandations.TabIndex = 0
        ' 
        ' pnlTopSearchRecommandations
        ' 
        pnlTopSearchRecommandations.Controls.Add(lblCountRecommandations)
        pnlTopSearchRecommandations.Controls.Add(chkSearchNotes)
        pnlTopSearchRecommandations.Controls.Add(chkRecommandationsActifs)
        pnlTopSearchRecommandations.Controls.Add(btnClearSearchRecommandations)
        pnlTopSearchRecommandations.Controls.Add(btnSearchRecommandations)
        pnlTopSearchRecommandations.Controls.Add(txtSearchRecommandations)
        pnlTopSearchRecommandations.Controls.Add(lblSearchRecommandations)
        pnlTopSearchRecommandations.Dock = DockStyle.Fill
        pnlTopSearchRecommandations.Location = New Point(0, 0)
        pnlTopSearchRecommandations.Margin = New Padding(0)
        pnlTopSearchRecommandations.Name = "pnlTopSearchRecommandations"
        pnlTopSearchRecommandations.Size = New Size(599, 69)
        pnlTopSearchRecommandations.TabIndex = 0
        ' 
        ' lblCountRecommandations
        ' 
        lblCountRecommandations.AutoSize = True
        lblCountRecommandations.Location = New Point(12, 46)
        lblCountRecommandations.Margin = New Padding(4, 0, 4, 0)
        lblCountRecommandations.Name = "lblCountRecommandations"
        lblCountRecommandations.Size = New Size(108, 15)
        lblCountRecommandations.TabIndex = 6
        lblCountRecommandations.Text = "0 recommandation"
        ' 
        ' chkSearchNotes
        ' 
        chkSearchNotes.AutoSize = True
        chkSearchNotes.Location = New Point(292, 46)
        chkSearchNotes.Margin = New Padding(4, 3, 4, 3)
        chkSearchNotes.Name = "chkSearchNotes"
        chkSearchNotes.Size = New Size(162, 19)
        chkSearchNotes.TabIndex = 5
        chkSearchNotes.Text = "Rechercher dans les notes"
        chkSearchNotes.UseVisualStyleBackColor = True
        ' 
        ' chkRecommandationsActifs
        ' 
        chkRecommandationsActifs.AutoSize = True
        chkRecommandationsActifs.Location = New Point(408, 12)
        chkRecommandationsActifs.Margin = New Padding(4, 3, 4, 3)
        chkRecommandationsActifs.Name = "chkRecommandationsActifs"
        chkRecommandationsActifs.Size = New Size(85, 19)
        chkRecommandationsActifs.TabIndex = 4
        chkRecommandationsActifs.Text = "Actifs seuls"
        chkRecommandationsActifs.UseVisualStyleBackColor = True
        ' 
        ' btnClearSearchRecommandations
        ' 
        btnClearSearchRecommandations.Location = New Point(373, 9)
        btnClearSearchRecommandations.Margin = New Padding(4, 3, 4, 3)
        btnClearSearchRecommandations.Name = "btnClearSearchRecommandations"
        btnClearSearchRecommandations.Size = New Size(28, 27)
        btnClearSearchRecommandations.TabIndex = 3
        btnClearSearchRecommandations.Text = "✖"
        btnClearSearchRecommandations.UseVisualStyleBackColor = True
        ' 
        ' btnSearchRecommandations
        ' 
        btnSearchRecommandations.Location = New Point(338, 9)
        btnSearchRecommandations.Margin = New Padding(4, 3, 4, 3)
        btnSearchRecommandations.Name = "btnSearchRecommandations"
        btnSearchRecommandations.Size = New Size(28, 27)
        btnSearchRecommandations.TabIndex = 2
        btnSearchRecommandations.Text = "🔍"
        btnSearchRecommandations.UseVisualStyleBackColor = True
        ' 
        ' txtSearchRecommandations
        ' 
        txtSearchRecommandations.Location = New Point(88, 12)
        txtSearchRecommandations.Margin = New Padding(4, 3, 4, 3)
        txtSearchRecommandations.Name = "txtSearchRecommandations"
        txtSearchRecommandations.Size = New Size(243, 23)
        txtSearchRecommandations.TabIndex = 1
        ' 
        ' lblSearchRecommandations
        ' 
        lblSearchRecommandations.AutoSize = True
        lblSearchRecommandations.Location = New Point(12, 15)
        lblSearchRecommandations.Margin = New Padding(4, 0, 4, 0)
        lblSearchRecommandations.Name = "lblSearchRecommandations"
        lblSearchRecommandations.Size = New Size(62, 15)
        lblSearchRecommandations.TabIndex = 0
        lblSearchRecommandations.Text = "Recherche"
        ' 
        ' dgvRecommandations
        ' 
        dgvRecommandations.AllowUserToAddRows = False
        dgvRecommandations.AllowUserToDeleteRows = False
        dgvRecommandations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRecommandations.Dock = DockStyle.Fill
        dgvRecommandations.Location = New Point(0, 69)
        dgvRecommandations.Margin = New Padding(0)
        dgvRecommandations.MultiSelect = False
        dgvRecommandations.Name = "dgvRecommandations"
        dgvRecommandations.ReadOnly = True
        dgvRecommandations.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRecommandations.Size = New Size(599, 208)
        dgvRecommandations.TabIndex = 1
        ' 
        ' tlpRecommandationDetails
        ' 
        tlpRecommandationDetails.ColumnCount = 2
        tlpRecommandationDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140F))
        tlpRecommandationDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpRecommandationDetails.Controls.Add(lblIdRecommandation, 0, 0)
        tlpRecommandationDetails.Controls.Add(txtIdRecommandation, 1, 0)
        tlpRecommandationDetails.Controls.Add(lblCodeRecommandation, 0, 1)
        tlpRecommandationDetails.Controls.Add(txtCodeRecommandation, 1, 1)
        tlpRecommandationDetails.Controls.Add(lblSourceNom, 0, 2)
        tlpRecommandationDetails.Controls.Add(txtSourceNom, 1, 2)
        tlpRecommandationDetails.Controls.Add(lblSourceLogin, 0, 3)
        tlpRecommandationDetails.Controls.Add(txtSourceLogin, 1, 3)
        tlpRecommandationDetails.Controls.Add(lblSourceUrl, 0, 4)
        tlpRecommandationDetails.Controls.Add(txtSourceUrl, 1, 4)
        tlpRecommandationDetails.Controls.Add(lblDateRecommandation, 0, 5)
        tlpRecommandationDetails.Controls.Add(dtpDateRecommandation, 1, 5)
        tlpRecommandationDetails.Controls.Add(chkRecommandationActive, 1, 6)
        tlpRecommandationDetails.Controls.Add(lblCommentaire, 0, 7)
        tlpRecommandationDetails.Controls.Add(pnlRichTextContainer, 1, 7)
        tlpRecommandationDetails.Dock = DockStyle.Fill
        tlpRecommandationDetails.Location = New Point(0, 277)
        tlpRecommandationDetails.Margin = New Padding(0)
        tlpRecommandationDetails.Name = "tlpRecommandationDetails"
        tlpRecommandationDetails.RowCount = 8
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRecommandationDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpRecommandationDetails.Size = New Size(599, 496)
        tlpRecommandationDetails.TabIndex = 2
        ' 
        ' lblIdRecommandation
        ' 
        lblIdRecommandation.Anchor = AnchorStyles.Left
        lblIdRecommandation.AutoSize = True
        lblIdRecommandation.Location = New Point(4, 7)
        lblIdRecommandation.Margin = New Padding(4, 0, 4, 0)
        lblIdRecommandation.Name = "lblIdRecommandation"
        lblIdRecommandation.Size = New Size(18, 15)
        lblIdRecommandation.TabIndex = 0
        lblIdRecommandation.Text = "ID"
        ' 
        ' txtIdRecommandation
        ' 
        txtIdRecommandation.Anchor = AnchorStyles.Left
        txtIdRecommandation.Location = New Point(144, 3)
        txtIdRecommandation.Margin = New Padding(4, 3, 4, 3)
        txtIdRecommandation.Name = "txtIdRecommandation"
        txtIdRecommandation.ReadOnly = True
        txtIdRecommandation.Size = New Size(116, 23)
        txtIdRecommandation.TabIndex = 1
        txtIdRecommandation.TabStop = False
        ' 
        ' lblCodeRecommandation
        ' 
        lblCodeRecommandation.Anchor = AnchorStyles.Left
        lblCodeRecommandation.AutoSize = True
        lblCodeRecommandation.Location = New Point(4, 37)
        lblCodeRecommandation.Margin = New Padding(4, 0, 4, 0)
        lblCodeRecommandation.Name = "lblCodeRecommandation"
        lblCodeRecommandation.Size = New Size(35, 15)
        lblCodeRecommandation.TabIndex = 2
        lblCodeRecommandation.Text = "Code"
        ' 
        ' txtCodeRecommandation
        ' 
        txtCodeRecommandation.Anchor = AnchorStyles.Left
        txtCodeRecommandation.Location = New Point(144, 33)
        txtCodeRecommandation.Margin = New Padding(4, 3, 4, 3)
        txtCodeRecommandation.Name = "txtCodeRecommandation"
        txtCodeRecommandation.ReadOnly = True
        txtCodeRecommandation.Size = New Size(116, 23)
        txtCodeRecommandation.TabIndex = 3
        txtCodeRecommandation.TabStop = False
        ' 
        ' lblSourceNom
        ' 
        lblSourceNom.Anchor = AnchorStyles.Left
        lblSourceNom.AutoSize = True
        lblSourceNom.Location = New Point(4, 67)
        lblSourceNom.Margin = New Padding(4, 0, 4, 0)
        lblSourceNom.Name = "lblSourceNom"
        lblSourceNom.Size = New Size(71, 15)
        lblSourceNom.TabIndex = 4
        lblSourceNom.Text = "Source nom"
        ' 
        ' txtSourceNom
        ' 
        txtSourceNom.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtSourceNom.Location = New Point(144, 63)
        txtSourceNom.Margin = New Padding(4, 3, 4, 3)
        txtSourceNom.Name = "txtSourceNom"
        txtSourceNom.Size = New Size(451, 23)
        txtSourceNom.TabIndex = 5
        ' 
        ' lblSourceLogin
        ' 
        lblSourceLogin.Anchor = AnchorStyles.Left
        lblSourceLogin.AutoSize = True
        lblSourceLogin.Location = New Point(4, 97)
        lblSourceLogin.Margin = New Padding(4, 0, 4, 0)
        lblSourceLogin.Name = "lblSourceLogin"
        lblSourceLogin.Size = New Size(73, 15)
        lblSourceLogin.TabIndex = 6
        lblSourceLogin.Text = "Source login"
        ' 
        ' txtSourceLogin
        ' 
        txtSourceLogin.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtSourceLogin.Location = New Point(144, 93)
        txtSourceLogin.Margin = New Padding(4, 3, 4, 3)
        txtSourceLogin.Name = "txtSourceLogin"
        txtSourceLogin.Size = New Size(451, 23)
        txtSourceLogin.TabIndex = 7
        ' 
        ' lblSourceUrl
        ' 
        lblSourceUrl.Anchor = AnchorStyles.Left
        lblSourceUrl.AutoSize = True
        lblSourceUrl.Location = New Point(4, 127)
        lblSourceUrl.Margin = New Padding(4, 0, 4, 0)
        lblSourceUrl.Name = "lblSourceUrl"
        lblSourceUrl.Size = New Size(60, 15)
        lblSourceUrl.TabIndex = 8
        lblSourceUrl.Text = "Source url"
        ' 
        ' txtSourceUrl
        ' 
        txtSourceUrl.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtSourceUrl.Location = New Point(144, 123)
        txtSourceUrl.Margin = New Padding(4, 3, 4, 3)
        txtSourceUrl.Name = "txtSourceUrl"
        txtSourceUrl.Size = New Size(451, 23)
        txtSourceUrl.TabIndex = 9
        ' 
        ' lblDateRecommandation
        ' 
        lblDateRecommandation.Anchor = AnchorStyles.Left
        lblDateRecommandation.AutoSize = True
        lblDateRecommandation.Location = New Point(4, 157)
        lblDateRecommandation.Margin = New Padding(4, 0, 4, 0)
        lblDateRecommandation.Name = "lblDateRecommandation"
        lblDateRecommandation.Size = New Size(31, 15)
        lblDateRecommandation.TabIndex = 10
        lblDateRecommandation.Text = "Date"
        ' 
        ' dtpDateRecommandation
        ' 
        dtpDateRecommandation.Anchor = AnchorStyles.Left
        dtpDateRecommandation.Format = DateTimePickerFormat.Short
        dtpDateRecommandation.Location = New Point(144, 153)
        dtpDateRecommandation.Margin = New Padding(4, 3, 4, 3)
        dtpDateRecommandation.Name = "dtpDateRecommandation"
        dtpDateRecommandation.ShowCheckBox = True
        dtpDateRecommandation.Size = New Size(174, 23)
        dtpDateRecommandation.TabIndex = 11
        ' 
        ' chkRecommandationActive
        ' 
        chkRecommandationActive.Anchor = AnchorStyles.Left
        chkRecommandationActive.AutoSize = True
        chkRecommandationActive.Location = New Point(144, 185)
        chkRecommandationActive.Margin = New Padding(4, 3, 4, 3)
        chkRecommandationActive.Name = "chkRecommandationActive"
        chkRecommandationActive.Size = New Size(59, 19)
        chkRecommandationActive.TabIndex = 12
        chkRecommandationActive.Text = "Active"
        chkRecommandationActive.UseVisualStyleBackColor = True
        ' 
        ' lblCommentaire
        ' 
        lblCommentaire.Anchor = AnchorStyles.Left
        lblCommentaire.AutoSize = True
        lblCommentaire.Location = New Point(4, 345)
        lblCommentaire.Margin = New Padding(4, 0, 4, 0)
        lblCommentaire.Name = "lblCommentaire"
        lblCommentaire.Size = New Size(80, 15)
        lblCommentaire.TabIndex = 13
        lblCommentaire.Text = "Commentaire"
        ' 
        ' pnlRichTextContainer
        ' 
        pnlRichTextContainer.Controls.Add(rtbCommentaire)
        pnlRichTextContainer.Controls.Add(ucRichTextToolbar)
        pnlRichTextContainer.Dock = DockStyle.Fill
        pnlRichTextContainer.Location = New Point(140, 210)
        pnlRichTextContainer.Margin = New Padding(0)
        pnlRichTextContainer.Name = "pnlRichTextContainer"
        pnlRichTextContainer.Size = New Size(459, 286)
        pnlRichTextContainer.TabIndex = 14
        ' 
        ' rtbCommentaire
        ' 
        rtbCommentaire.Dock = DockStyle.Fill
        rtbCommentaire.Location = New Point(0, 35)
        rtbCommentaire.Margin = New Padding(4, 3, 4, 3)
        rtbCommentaire.Name = "rtbCommentaire"
        rtbCommentaire.Size = New Size(459, 251)
        rtbCommentaire.TabIndex = 1
        rtbCommentaire.Text = ""
        ' 
        ' ucRichTextToolbar
        ' 
        ucRichTextToolbar.Dock = DockStyle.Top
        ucRichTextToolbar.Location = New Point(0, 0)
        ucRichTextToolbar.Margin = New Padding(4, 3, 4, 3)
        ucRichTextToolbar.Name = "ucRichTextToolbar"
        ucRichTextToolbar.Size = New Size(459, 35)
        ucRichTextToolbar.TabIndex = 0
        ucRichTextToolbar.TargetRichTextBox = Nothing
        ' 
        ' pnlActionsRecommandation
        ' 
        pnlActionsRecommandation.Controls.Add(btnNewRecommandation)
        pnlActionsRecommandation.Controls.Add(btnEditRecommandation)
        pnlActionsRecommandation.Controls.Add(btnSaveRecommandation)
        pnlActionsRecommandation.Controls.Add(btnCancelRecommandation)
        pnlActionsRecommandation.Controls.Add(btnDeleteRecommandation)
        pnlActionsRecommandation.Dock = DockStyle.Fill
        pnlActionsRecommandation.Location = New Point(0, 773)
        pnlActionsRecommandation.Margin = New Padding(0)
        pnlActionsRecommandation.Name = "pnlActionsRecommandation"
        pnlActionsRecommandation.Size = New Size(599, 35)
        pnlActionsRecommandation.TabIndex = 3
        ' 
        ' btnNewRecommandation
        ' 
        btnNewRecommandation.Location = New Point(4, 3)
        btnNewRecommandation.Margin = New Padding(4, 3, 4, 3)
        btnNewRecommandation.Name = "btnNewRecommandation"
        btnNewRecommandation.Size = New Size(88, 27)
        btnNewRecommandation.TabIndex = 0
        btnNewRecommandation.Text = "Nouveau"
        btnNewRecommandation.UseVisualStyleBackColor = True
        ' 
        ' btnEditRecommandation
        ' 
        btnEditRecommandation.Location = New Point(98, 3)
        btnEditRecommandation.Margin = New Padding(4, 3, 4, 3)
        btnEditRecommandation.Name = "btnEditRecommandation"
        btnEditRecommandation.Size = New Size(88, 27)
        btnEditRecommandation.TabIndex = 1
        btnEditRecommandation.Text = "Modifier"
        btnEditRecommandation.UseVisualStyleBackColor = True
        ' 
        ' btnSaveRecommandation
        ' 
        btnSaveRecommandation.Location = New Point(192, 3)
        btnSaveRecommandation.Margin = New Padding(4, 3, 4, 3)
        btnSaveRecommandation.Name = "btnSaveRecommandation"
        btnSaveRecommandation.Size = New Size(88, 27)
        btnSaveRecommandation.TabIndex = 2
        btnSaveRecommandation.Text = "Enregistrer"
        btnSaveRecommandation.UseVisualStyleBackColor = True
        ' 
        ' btnCancelRecommandation
        ' 
        btnCancelRecommandation.Location = New Point(287, 3)
        btnCancelRecommandation.Margin = New Padding(4, 3, 4, 3)
        btnCancelRecommandation.Name = "btnCancelRecommandation"
        btnCancelRecommandation.Size = New Size(88, 27)
        btnCancelRecommandation.TabIndex = 3
        btnCancelRecommandation.Text = "Annuler"
        btnCancelRecommandation.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteRecommandation
        ' 
        btnDeleteRecommandation.Location = New Point(382, 3)
        btnDeleteRecommandation.Margin = New Padding(4, 3, 4, 3)
        btnDeleteRecommandation.Name = "btnDeleteRecommandation"
        btnDeleteRecommandation.Size = New Size(88, 27)
        btnDeleteRecommandation.TabIndex = 4
        btnDeleteRecommandation.Text = "Supprimer"
        btnDeleteRecommandation.UseVisualStyleBackColor = True
        ' 
        ' UC_Recommandations
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(tlpCenter)
        Margin = New Padding(4, 3, 4, 3)
        Name = "UC_Recommandations"
        Size = New Size(1198, 836)
        tlpCenter.ResumeLayout(False)
        pnlLeftOrigines.ResumeLayout(False)
        grpOrigineDetails.ResumeLayout(False)
        tlpOrigineDetails.ResumeLayout(False)
        tlpOrigineDetails.PerformLayout()
        CType(nudOrdreOrigine, ComponentModel.ISupportInitialize).EndInit()
        pnlActionsOrigine.ResumeLayout(False)
        CType(dgvOrigines, ComponentModel.ISupportInitialize).EndInit()
        pnlTopSearchOrigines.ResumeLayout(False)
        pnlTopSearchOrigines.PerformLayout()
        pnlRightDetails.ResumeLayout(False)
        grpRecommandations.ResumeLayout(False)
        tlpRecommandations.ResumeLayout(False)
        pnlTopSearchRecommandations.ResumeLayout(False)
        pnlTopSearchRecommandations.PerformLayout()
        CType(dgvRecommandations, ComponentModel.ISupportInitialize).EndInit()
        tlpRecommandationDetails.ResumeLayout(False)
        tlpRecommandationDetails.PerformLayout()
        pnlRichTextContainer.ResumeLayout(False)
        pnlActionsRecommandation.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents pnlLeftOrigines As Panel
    Friend WithEvents dgvOrigines As DataGridView
    Friend WithEvents pnlTopSearchOrigines As Panel
    Friend WithEvents lblCountOrigines As Label
    Friend WithEvents chkOriginesActifs As CheckBox
    Friend WithEvents btnClearSearchOrigines As Button
    Friend WithEvents btnSearchOrigines As Button
    Friend WithEvents txtSearchOrigines As TextBox
    Friend WithEvents lblSearchOrigines As Label
    Friend WithEvents pnlRightDetails As Panel
    Friend WithEvents grpOrigineDetails As GroupBox
    Friend WithEvents tlpOrigineDetails As TableLayoutPanel
    Friend WithEvents lblIdOrigine As Label
    Friend WithEvents txtIdOrigine As TextBox
    Friend WithEvents lblCodeOrigine As Label
    Friend WithEvents txtCodeOrigine As TextBox
    Friend WithEvents lblLibelleOrigine As Label
    Friend WithEvents txtLibelleOrigine As TextBox
    Friend WithEvents lblOrdreOrigine As Label
    Friend WithEvents nudOrdreOrigine As NumericUpDown
    Friend WithEvents chkOrigineActif As CheckBox
    Friend WithEvents pnlActionsOrigine As Panel
    Friend WithEvents btnNewOrigine As Button
    Friend WithEvents btnEditOrigine As Button
    Friend WithEvents btnSaveOrigine As Button
    Friend WithEvents btnCancelOrigine As Button
    Friend WithEvents btnDeleteOrigine As Button
    Friend WithEvents grpRecommandations As GroupBox
    Friend WithEvents tlpRecommandations As TableLayoutPanel
    Friend WithEvents pnlTopSearchRecommandations As Panel
    Friend WithEvents lblCountRecommandations As Label
    Friend WithEvents chkSearchNotes As CheckBox
    Friend WithEvents chkRecommandationsActifs As CheckBox
    Friend WithEvents btnClearSearchRecommandations As Button
    Friend WithEvents btnSearchRecommandations As Button
    Friend WithEvents txtSearchRecommandations As TextBox
    Friend WithEvents lblSearchRecommandations As Label
    Friend WithEvents dgvRecommandations As DataGridView
    Friend WithEvents tlpRecommandationDetails As TableLayoutPanel
    Friend WithEvents lblIdRecommandation As Label
    Friend WithEvents txtIdRecommandation As TextBox
    Friend WithEvents lblCodeRecommandation As Label
    Friend WithEvents txtCodeRecommandation As TextBox
    Friend WithEvents lblSourceNom As Label
    Friend WithEvents txtSourceNom As TextBox
    Friend WithEvents lblSourceLogin As Label
    Friend WithEvents txtSourceLogin As TextBox
    Friend WithEvents lblSourceUrl As Label
    Friend WithEvents txtSourceUrl As TextBox
    Friend WithEvents lblDateRecommandation As Label
    Friend WithEvents dtpDateRecommandation As DateTimePicker
    Friend WithEvents chkRecommandationActive As CheckBox
    Friend WithEvents lblCommentaire As Label
    Friend WithEvents pnlRichTextContainer As Panel
    Friend WithEvents rtbCommentaire As RichTextBox
    Friend WithEvents ucRichTextToolbar As UC_RichTextToolbar
    Friend WithEvents pnlActionsRecommandation As Panel
    Friend WithEvents btnNewRecommandation As Button
    Friend WithEvents btnEditRecommandation As Button
    Friend WithEvents btnSaveRecommandation As Button
    Friend WithEvents btnCancelRecommandation As Button
    Friend WithEvents btnDeleteRecommandation As Button
End Class
