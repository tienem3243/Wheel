using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class FileBrowserHelper
{
    public static string OpenFilePanel(Action<string> action)
    {
        string pathName = "";
        // Open a file panel
        SimpleFileBrowser.FileBrowser.ShowLoadDialog((paths) =>
        {
           
            // Check if any files are selected
            if (paths != null && paths.Length > 0)
            {
                // Save the first file to Unity project
                SaveFileToUnityProject(paths[0]);
                 pathName = System.IO.Path.GetFileName(paths[0]);
                action.Invoke(pathName);
            }
            else
            {
                Debug.Log("Không có file được chọn");
            }
        }, null, SimpleFileBrowser.FileBrowser.PickMode.Files, false, "false", null, "Chọn file", "Chọn");
      
        return pathName; // Placeholder, actual return handled in the callback
    }

    private static void SaveFileToUnityProject(string filePath)
    {
        string fileName = System.IO.Path.GetFileName(filePath);
        string destinationPath = "Assets/Resources/" + fileName;

        // Copy the file to the Unity project folder
        System.IO.File.Copy(filePath, destinationPath, true);

        // Importing to AssetDatabase not needed for runtime loading
        // UnityEditor.AssetDatabase.ImportAsset(destinationPath, UnityEditor.ImportAssetOptions.Default);

        Debug.Log("File đã được sao chép vào thư mục Resources.");
    }
}
