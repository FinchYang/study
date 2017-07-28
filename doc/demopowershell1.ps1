$processList = New-Object System.Collections.ArrayList
#Get all processes.
$allProcesses = Get-Process | Select Id,Name,Path,Description,VM,WS,CPU,Company | sort -Property Name
#Add all processes to an array list which is easier to manipulate.
$processList.AddRange($allProcesses)
#Instantiate a new Windows Form
$form = New-Object Windows.Forms.Form
#Sets the Windows Form size and start position.
$form.Size=New-Object Drawing.Size @(800,600)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::CenterScreen
#This will create panels to display our items.
$panelLabel = New-Object Windows.Forms.Panel
$panelMain = New-Object Windows.Forms.Panel
$panelButton = New-Object Windows.Forms.Panel

#Creates the save file dialog so that we can export it as CSV.
$saveDialog = new-object System.Windows.Forms.SaveFileDialog
$saveDialog.DefaultExt = ".csv"
$saveDialog.AddExtension = $true
#Create the save to CSV button.
$buttonSave = New-Object Windows.Forms.Button
$buttonSave.Text = "Save as CSV"
$buttonSave.Left = 100
$buttonSave.Width =100
#Add the OnClick save button event.
$buttonSave.add_Click(
{
$resultSave=$saveDialog.ShowDialog()
#If the user clicks ok to save.
if($resultSave -eq "OK"){
#Save as CSV
$allProcesses | Export-Csv -Path $saveDialog.FileName
MessageBox("Guardado com sucesso")
}
})
#Create the exit application button.
$button = New-Object Windows.Forms.Button
$button.Text = "Exit"
$button.Left = 10
#Add the OnClick exit button event.
$button.add_Click(
{
$form.Close()
})
#Create datagrid
$dataGrid=New-Object Windows.Forms.DataGrid
$dataGrid.Dock = "Fill"
$dataGrid.DataSource = $processList
#Create a new label to show on the header.
$label = New-Object System.Windows.Forms.Label
$label.Text= "Demo UI using Powershell - Rui Machado 2013 for SyncFusion"
$label.Font = "Segoe UI Light"
$label.Width= 300
#Add the header label to its panel.
$panelLabel.Controls.Add($label)
$panelLabel.Height =35
$panelLabel.Dock = "Top"

$panelLabel.BackColor = "White"
#Add datagrid to its panel.
$panelMain.Controls.Add($dataGrid)
$panelMain.Height =470
$panelMain.Dock = "Top"
#Adds buttons to its panel.
$panelButton.Controls.Add($button)
$panelButton.Controls.Add($buttonSave)
$panelButton.Height=50
$panelButton.Dock = "Bottom"
#Add all panels to the form.
$form.Controls.Add($panelMain)
$form.Controls.Add($panelButton)
$form.Controls.Add($panelLabel)
$form.Refresh()
#Show the form.
$result = $form.ShowDialog() | Out-Null
if($result -eq "Cancel")
{
MessageBox("Program is closing...")
$form.Close() 
}
#OPTIONAL: Function to create new MessageBoxes
function MessageBox([string]$msgToShow)
{
[System.Windows.Forms.MessageBox]::Show($msgToShow)
}