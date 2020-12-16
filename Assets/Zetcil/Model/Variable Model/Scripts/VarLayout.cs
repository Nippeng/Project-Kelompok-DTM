using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;

namespace Zetcil
{
    public class VarLayout : MonoBehaviour
    {
        public enum COperationType { None, Initialize, Runtime }
        public enum CPresetType { Default, Vermilion, Emerald, Azure, Custom }

        [Space(10)]
        public bool isEnabled;

        [Header("Operation Settings")]
        public COperationType OperationType;

        [Header("Layout Settings")]
        public CPresetType PresetType;

        [Header("Header Color")]
        public Color HeaderColor;
        [Range(0, 1)] public float HeaderTransparency;

        [Header("Footer Color")]
        public Color FooterColor;
        [Range(0, 1)] public float FooterTransparency;

        [Header("Background Color")]
        public Color BackgroundColor;
        [Range(0, 1)] public float BackgroundTransparency;

        [Header("Panel Color")]
        public Color PanelColor;
        [Range(0, 1)] public float PanelTransparency;

        [Header("Icon Color")]
        public Color IconColor;
        [Range(0, 1)] public float IconTransparency;

        [Header("Primary Button Color")]
        public Color PrimaryNormalColor;
        public Color PrimaryHighlightColor;
        public Color PrimaryPressedColor;
        public Color PrimarySelectedColor;
        public Color PrimaryDisabledColor;
        [Range(0, 1)] public float PrimaryButtonTransparency;

        [Header("Secondary Button Color")]
        public Color SecondaryNormalColor;
        public Color SecondaryHighlightColor;
        public Color SecondaryPressedColor;
        public Color SecondarySelectedColor;
        public Color SecondaryDisabledColor;
        [Range(0, 1)] public float SecondaryButtonTransparency;

        string ConfigDirectory = "Config";
        string LayoutDirectory = "Layout";

        TextAsset layoutAsset;

        void LoadCurrentLayout(string aLayout)
        {
            if (aLayout == "VERMILION")
            {
                PresetType = CPresetType.Vermilion;
            }
            if (aLayout == "EMERALD")
            {
                PresetType = CPresetType.Emerald;
            }
            if (aLayout == "AZURE")
            {
                PresetType = CPresetType.Azure;
            }
            if (aLayout == "CUSTOM")
            {
                PresetType = CPresetType.Custom;
            }
            if (aLayout == "DEFAULT") 
            {
                PresetType = CPresetType.Default;
            }
        }
        string SaveCurrentLayout()
        {
            string result = "";
            if (PresetType == CPresetType.Default) result = "DEFAULT";
            if (PresetType == CPresetType.Vermilion) result = "VERMILION";
            if (PresetType == CPresetType.Emerald) result = "EMERALD";
            if (PresetType == CPresetType.Azure) result = "AZURE";
            if (PresetType == CPresetType.Custom) result = "CUSTOM";
            return result;
        }

        string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.streamingAssetsPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath + "/" + aDirectoryName + "/");
            }
            return Application.streamingAssetsPath + "/" + aDirectoryName + "/";
        }

        public Color ConvertColor(string aColor, float aTransparency)
        {
            Color result = Color.black;
            ColorUtility.TryParseHtmlString(aColor, out result);
            result.a = aTransparency;
            return result;
        }

        public void SaveLayoutFile(string aLayoutID, string aLayoutData)
        {
            string DirName = GetDirectory(LayoutDirectory);
            var sr = File.CreateText(DirName + aLayoutID + ".xml");
            sr.WriteLine(aLayoutData);
            sr.Close();
        }

        string SetRowLayout(string aColorID, Color aColorValue)
        {
            string result = "\t<" + aColorID + " Red=\""+ aColorValue.r.ToString() + "\" Green=\""+ aColorValue.g.ToString() + "\" Blue=\""+ aColorValue.b.ToString() + "\" Alpha=\"" + aColorValue.a.ToString() + "\">" +
                            "</" + aColorID + ">\n";
            return result;
        }

        public void SaveFile()
        {
            string header = "<LayoutSettings>\n";
            string footer = "</LayoutSettings>";
            string result = "";

            string opentag = "\t<" + "Layout" + ">\n";
            string contenttag = "\t\t" + SaveCurrentLayout() + "\n";
            string closetag = "\t</" + "Layout" + ">\n";

            string _HeadeColor = SetRowLayout("HeaderColor", HeaderColor);
            string _FooterColor = SetRowLayout("FooterColor", FooterColor);
            string _BackgroundColor = SetRowLayout("BackgroundColor", BackgroundColor);
            string _PanelColor = SetRowLayout("PanelColor", PanelColor);
            string _IconColor = SetRowLayout("IconColor", IconColor);
            string _PrimaryNormalColor = SetRowLayout("PrimaryNormalColor", PrimaryNormalColor);
            string _PrimaryHighlightColor = SetRowLayout("PrimaryHighlightColor", PrimaryHighlightColor);
            string _PrimaryPressedColor = SetRowLayout("PrimaryPressedColor", PrimaryPressedColor);
            string _PrimarySelectedColor = SetRowLayout("PrimarySelectedColor", PrimarySelectedColor);
            string _PrimaryDisabledColor = SetRowLayout("PrimaryDisabledColor", PrimaryDisabledColor);
            string _SecondaryNormalColor = SetRowLayout("SecondaryNormalColor", SecondaryNormalColor);
            string _SecondaryHighlightColor = SetRowLayout("SecondaryHighlightColor", SecondaryHighlightColor);
            string _SecondaryPressedColor = SetRowLayout("SecondaryPressedColor", SecondaryPressedColor);
            string _SecondarySelectedColor = SetRowLayout("SecondarySelectedColor", SecondarySelectedColor);
            string _SecondaryDisabledColor = SetRowLayout("SecondaryDisabledColor", SecondaryDisabledColor);

            result = opentag + contenttag + closetag +
                _HeadeColor + _FooterColor + _BackgroundColor + _PanelColor + _IconColor +
                _PrimaryNormalColor + _PrimaryHighlightColor + _PrimaryPressedColor + _PrimarySelectedColor + _PrimaryDisabledColor +
                _SecondaryNormalColor + _SecondaryHighlightColor + _SecondaryPressedColor + _SecondarySelectedColor + _SecondaryDisabledColor
                ;
            result = header + result + footer;

            string DirName = GetDirectory(ConfigDirectory);
            var sr = File.CreateText(DirName + "Layout.xml");
            sr.WriteLine(result);
            sr.Close();
        }

        public void LoadFile()
        {
            string FullPathFile = GetDirectory(ConfigDirectory) + "Layout.xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                XmlNode xmlnode;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName("Layout");
                LoadCurrentLayout(xmlnodelist.Item(0).InnerText.Trim());

                SetColorLayout(xmldoc, out HeaderColor, out HeaderTransparency, "HeaderColor");
                SetColorLayout(xmldoc, out FooterColor, out FooterTransparency, "FooterColor");
                SetColorLayout(xmldoc, out BackgroundColor, out BackgroundTransparency, "BackgroundColor");
                SetColorLayout(xmldoc, out PanelColor, out PanelTransparency, "PanelColor");
                SetColorLayout(xmldoc, out IconColor, out IconTransparency, "IconColor");
                SetColorLayout(xmldoc, out PrimaryNormalColor, out PrimaryButtonTransparency, "PrimaryNormalColor");
                SetColorLayout(xmldoc, out PrimaryPressedColor, out PrimaryButtonTransparency, "PrimaryPressedColor");
                SetColorLayout(xmldoc, out PrimarySelectedColor, out PrimaryButtonTransparency, "PrimarySelectedColor");
                SetColorLayout(xmldoc, out PrimaryDisabledColor, out PrimaryButtonTransparency, "PrimaryDisabledColor");
                SetColorLayout(xmldoc, out SecondaryNormalColor, out SecondaryButtonTransparency, "SecondaryNormalColor");
                SetColorLayout(xmldoc, out SecondaryHighlightColor, out SecondaryButtonTransparency, "SecondaryHighlightColor");
                SetColorLayout(xmldoc, out SecondaryPressedColor, out SecondaryButtonTransparency, "SecondaryPressedColor");
                SetColorLayout(xmldoc, out SecondarySelectedColor, out SecondaryButtonTransparency, "SecondarySelectedColor");
                SetColorLayout(xmldoc, out SecondaryDisabledColor, out SecondaryButtonTransparency, "SecondaryDisabledColor");

            }
        }

        void SetColorLayout(XmlDocument xmlthemedoc, out Color themeColor, out float themeTransparency, string themeParameter)
        {
            XmlNodeList xmlColor = xmlthemedoc.GetElementsByTagName(themeParameter);
            themeColor.r = float.Parse(xmlColor.Item(0).Attributes[0].Value);
            themeColor.g = float.Parse(xmlColor.Item(0).Attributes[1].Value);
            themeColor.b = float.Parse(xmlColor.Item(0).Attributes[2].Value);
            themeColor.a = float.Parse(xmlColor.Item(0).Attributes[3].Value);
            themeTransparency = float.Parse(xmlColor.Item(0).Attributes[3].Value);
        }

        public void SetLayoutDefault()
        {
            PresetType = CPresetType.Default;

            HeaderColor = ConvertColor("#43454B", HeaderTransparency);
            FooterColor = ConvertColor("#43454B", FooterTransparency);
            BackgroundColor = ConvertColor("#43454B", BackgroundTransparency);
            PanelColor = ConvertColor("#43454B", PanelTransparency);
            IconColor = ConvertColor("#43454B", IconTransparency);

            PrimaryNormalColor = ConvertColor("#43454B", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#9FA2AB", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#C8C9CD", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#AEB6C1", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#43454B", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#43454B", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#9FA2AB", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#C8C9CD", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#AEB6C1", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#43454B", SecondaryButtonTransparency);
        }

        public void SetLayoutVermilion()
        {
            PresetType = CPresetType.Vermilion;

            HeaderColor = ConvertColor("#9F243D", HeaderTransparency);
            FooterColor = ConvertColor("#9F243D", FooterTransparency);
            BackgroundColor = ConvertColor("#9F243D", BackgroundTransparency);
            PanelColor = ConvertColor("#9F243D", PanelTransparency);
            IconColor = ConvertColor("#9F243D", IconTransparency);

            PrimaryNormalColor = ConvertColor("#9F243D", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#FF9358", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#F8B997", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#F6AA83", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#9F243D", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#9F243D", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#FF9358", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#F8B997", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#F6AA83", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#9F243D", SecondaryButtonTransparency);
        }

        public void SetLayoutEmerald()
        {
            PresetType = CPresetType.Emerald;

            HeaderColor = ConvertColor("#3CC85A", HeaderTransparency);
            FooterColor = ConvertColor("#3CC85A", FooterTransparency);
            BackgroundColor = ConvertColor("#3CC85A", BackgroundTransparency);
            PanelColor = ConvertColor("#3CC85A", PanelTransparency);
            IconColor = ConvertColor("#3CC85A", IconTransparency);

            PrimaryNormalColor = ConvertColor("#3CC85A", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#BEC83B", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#DBE37F", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#D0D969", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#3CC85A", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#3CC85A", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#BEC83B", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#DBE37F", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#D0D969", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#3CC85A", SecondaryButtonTransparency);
        }

        public void SetLayoutAzure()
        {
            PresetType = CPresetType.Azure;

            HeaderColor = ConvertColor("#2845B3", HeaderTransparency);
            FooterColor = ConvertColor("#2845B3", FooterTransparency);
            BackgroundColor = ConvertColor("#2845B3", BackgroundTransparency);
            PanelColor = ConvertColor("#2845B3", PanelTransparency);
            IconColor = ConvertColor("#2845B3", IconTransparency);

            PrimaryNormalColor = ConvertColor("#2845B3", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#ABAB2B", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#FFFBE6", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#ADBCCF", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#2845B3", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#2845B3", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#ABAB2B", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#FFFBE6", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#ADBCCF", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#2845B3", SecondaryButtonTransparency);
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (isEnabled)
            {
                if (OperationType == COperationType.Initialize)
                {
                    SaveFile();
                }
                if (OperationType == COperationType.Runtime)
                {
                    LoadFile();
                }
                if (PresetType == CPresetType.Default)
                {
                    SetLayoutDefault();
                }
                else if (PresetType == CPresetType.Vermilion)
                {
                    SetLayoutVermilion();
                }
                else if (PresetType == CPresetType.Emerald)
                {
                    SetLayoutEmerald();
                }
                else if (PresetType == CPresetType.Azure)
                {
                    SetLayoutAzure();
                }
            }
        }

    }
}
