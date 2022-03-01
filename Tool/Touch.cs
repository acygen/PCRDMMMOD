using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using PCRCalculator.Tool;
using HarmonyLib;
using Elements;


namespace PCRCalculator.Hook
{
    //[HarmonyPatch(typeof(TouchManager), "Mouse")]
    class Touch
    {
        static Dictionary<int, Vector3> touchPosDic = new Dictionary<int, Vector3>
        {
            {0,new Vector3(959,132,0) },
            {1,new Vector3(793,132,0) },
            {2,new Vector3(643,132,0) },
            {3,new Vector3(488,132,0) },
            {4,new Vector3(319,132,0) },
            {5,new Vector3(1197,686,0) },
            {6,new Vector3(1213,150,0) },
            {7,new Vector3(1213,54,0) },

        };
        static List<TouchManager.TouchData> touchDatas = new List<TouchManager.TouchData>();
        static void Postfix(TouchManager __instance)
        {
            TouchManager.TouchData[] _touchDatas = Traverse.Create(__instance).Field("_touchDatas").GetValue<TouchManager.TouchData[]>();

            if (PCRBattle.Instance.saveData.useFastKey)
            {
                int[] keys = PCRBattle.Instance.saveData.fastKeys;
                Traverse.Create(__instance).Field("_touchCount").SetValue(0);

                if (touchDatas.Count > 0)
                {
                    _touchDatas[0]._pos = touchDatas[0]._pos;
                    _touchDatas[0]._type = touchDatas[0]._type;
                    touchDatas.RemoveAt(0);

                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (Input.GetKey((KeyCode)keys[i]))
                        {
                            _touchDatas[0]._pos = touchPosDic[i];
                            _touchDatas[0]._type = TouchManager.eTouch.Down;
                            Traverse.Create(__instance).Field("_touchFirstPos").SetValue(touchPosDic[i]);
                            //string log = "TOUCH:" + _touchDatas[0]._pos.x + "--" + _touchDatas[0]._pos.y + "--" + _touchDatas[0]._pos.z + "->" + _touchDatas[0]._type.GetDescription();
                            //Cute.ClientLog.AccumulateClientLog(log); 
                            //touchDatas.Add(new TouchManager.TouchData { _type = TouchManager.eTouch.Press,  _fingerID = _touchDatas[0]._fingerID, _oldPos = _touchDatas[0]._oldPos, _pos = _touchDatas[0]._pos });
                            touchDatas.Add(new TouchManager.TouchData { _type = TouchManager.eTouch.Up, _fingerID = _touchDatas[0]._fingerID, _oldPos = _touchDatas[0]._oldPos, _pos = _touchDatas[0]._pos });
                            break;
                        }
                    }
                    for (int i = 5; i < 8; i++)
                    {
                        if (Input.GetKeyDown((KeyCode)keys[i]))
                        {
                            _touchDatas[0]._pos = touchPosDic[i];
                            _touchDatas[0]._type = TouchManager.eTouch.Down;
                            Traverse.Create(__instance).Field("_touchFirstPos").SetValue(touchPosDic[i]);
                            //string log = "TOUCH:" + _touchDatas[0]._pos.x + "--" + _touchDatas[0]._pos.y + "--" + _touchDatas[0]._pos.z + "->" + _touchDatas[0]._type.GetDescription();
                            //Cute.ClientLog.AccumulateClientLog(log); 
                            //touchDatas.Add(new TouchManager.TouchData { _type = TouchManager.eTouch.Press, _fingerID = _touchDatas[0]._fingerID, _oldPos = _touchDatas[0]._oldPos, _pos = _touchDatas[0]._pos });
                            touchDatas.Add(new TouchManager.TouchData { _type = TouchManager.eTouch.Up, _fingerID = _touchDatas[0]._fingerID, _oldPos = _touchDatas[0]._oldPos, _pos = _touchDatas[0]._pos });
                            break;
                        }
                    }
                }

            }
            //TouchManager.TouchData[] _touchDatas = Traverse.Create(__instance).Field("_touchDatas").GetValue<TouchManager.TouchData[]>();
            if (_touchDatas[0]._type == TouchManager.eTouch.None)
                return;
            string log = "TOUCH:" + _touchDatas[0]._pos.x + "--" + _touchDatas[0]._pos.y + "--" + _touchDatas[0]._pos.z + "->" + _touchDatas[0]._type.GetDescription();
            Cute.ClientLog.AccumulateClientLog(log);

        }

    }

    //[HarmonyPatch(typeof(UnitUiCtrl), "Mouse")]
    class UICtrl
    {

    }
}
