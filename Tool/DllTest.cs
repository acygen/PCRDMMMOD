using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace PCRCalculator.Tool
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

    public class OpenFileName
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;

    }




    public class DllTest
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
        public static bool GetOpenFileName1([In, Out] OpenFileName ofn)
        {
            return GetOpenFileName(ofn);
        }

        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
        public static bool GetSaveFileName1([In, Out] OpenFileName ofn)
        {
            return GetSaveFileName(ofn);
        }
    }
    public static class System_Windows_Froms
    {
        //public static    //接受选择文件的路径
        //public static    //接受转成功后的路径 也就是Unity所需要的路径
        //自定义文件保存文件夹;
        public static string GetChoosedPath(string title = "选择文件夹")
        {
            string CompentPath = "";
            string UnityPath = "";
            FolderBrowserDialog fb = new FolderBrowserDialog();   //创建控件并实例化
            fb.Description = title;
            fb.RootFolder = Environment.SpecialFolder.MyComputer;  //设置默认路径
            fb.ShowNewFolderButton = false;   //创建文件夹按钮关闭
                                              //如果按下弹窗的OK按钮
            if (fb.ShowDialog() == DialogResult.OK)
            {
                //接受路径
                CompentPath = fb.SelectedPath;
            }
            //将路径中的 \ 替换成 /            由于unity路径的规范必须转
            //UnityPath = CompentPath.Replace(@"\", "/");
            fb.Dispose();
            return CompentPath;
            //print(UnityPath);
            //如果 \  比较多的话      
            //if (UnityPath.IndexOf("/") > 2)
            //｛
            //UnityPath = CompentPath+ "/";
            //print("大于了");
            //｝
            //else ｛
            //print("小于了");
            //｝
        }
    }
}