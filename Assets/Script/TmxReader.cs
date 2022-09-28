using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmxReader : MonoBehaviour
{
    void Start()
    {
        TextAsset text = Resources.Load<TextAsset>(string.Format("{0}/{1}", "Map", "Level"));  // Assets/Resources/Map
        JSONNode data = JSONNode.Parse(text.text).AsObject;//将tmx文本转化为json对象
        Debug.Log(data["layernumber"]);
        //foreach (var layer in data["layers"])
        for (int cur_layer = 0; cur_layer < data["layernumber"]; cur_layer++)//["type"] ==  "objectgroup"   "tilelayer"
        {
            Dictionary<string, JSONNode> objectsDic = new Dictionary<string, JSONNode>();
            var layer = Instantiate(Resources.Load(string.Format("{0}/{1}", "Grids", "LayerPre"))) as GameObject;
            layer.transform.SetParent(transform);
            layer.name = data["layers"][cur_layer]["name"];
            //1、对象层操作
            if (string.Equals(data["layers"][cur_layer]["type"], "objectgroup"))
            {
                //string layers_objects = data["layers"][cur_layer]["objects"].ToString();
                //JSONArray arr_layers_objects = JSONNode.Parse(layers_objects).AsArray;
                int tilewidth = int.Parse(data["tilewidth"]);//获取格子宽
                int tileheight = int.Parse(data["tileheight"]);//获取格子高
                                                               // foreach (var obj in arr_layers_objects)//遍历所有对象层上的对象
                                                               // {
                                                               //     int key_x = obj.Value["x"] / tilewidth;//格子x轴 除以 格子宽得出这个对象在x轴第几个格子内
                                                               //     int key_y = obj.Value["y"] / tileheight;//格子y轴 除以 格子高 得出这个对象在y轴第几个格子内
                                                               //     objectsDic[string.Format("{0}-{1}", key_y, key_x)] = obj.Value["properties"];//将对象里的值保存到对应格子内
                                                               // }
            }
            //2、绘制层操作
            else if (string.Equals(data["layers"][cur_layer]["type"], "tilelayer"))
            {
                int draw_layer = cur_layer;
                string layers_data = data["layers"][draw_layer]["data"].ToString();//获取图层内的二维数组
                JSONArray arr_layers_data = JSONNode.Parse(layers_data).AsArray;
                JSONNode tileproperties = data["tilesets"][draw_layer]["tileproperties"];
                int col = int.Parse(data["width"]);
                int row = int.Parse(data["height"]);
                float sprite_size = 1f;
                // Vector3 vec = new Vector3(int.Parse((col / 2).ToString()) * sprite_size, int.Parse((row / 2).ToString()) * sprite_size, 0);
                Vector3 vec = new Vector3(0, int.Parse((row).ToString()) * sprite_size, 0);
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        int gid = arr_layers_data[j + i * col];
                        if (gid == 0)  //如果值为0则认为该格没有地块，跳过该格
                            continue;

                        var go = Instantiate(Resources.Load(string.Format("{0}/{1}", "Grids", "GridPre"))) as GameObject;
                        go.name = (string.Format("({0},{1})", j+1, i+1));
                        go.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Map/hexmini")[data["layers"][draw_layer]["data"][j + i * col] - 1];
                        go.GetComponent<SpriteRenderer>().sortingOrder = draw_layer;
                        go.transform.SetParent(layer.transform);

                        //parse tiled properties when drawing    ////not test yet!!!!!!
                        if (tileproperties)
                        {
                            int tilesets = int.Parse(tileproperties["1"]["ID"]);
                            JSONNode Dic_Grid = tileproperties[gid.ToString()];
                            go.name = string.Format("{0}_{1}_{2}", row - i - 1, j, int.Parse(Dic_Grid["ID"]));
                        }
                        if (objectsDic.ContainsKey(string.Format("{0}-{1}", i, j)))
                        {
                            var __objectsDic = objectsDic[string.Format("{0}-{1}", i, j)];

                            if (__objectsDic["ROW"] != null && __objectsDic["COL"] != null)
                            {
                                int __col = int.Parse(__objectsDic["COL"]);
                                int __row = int.Parse(__objectsDic["ROW"]);
                                var start = vec + new Vector3(sprite_size * j, -sprite_size * i, 0);
                                var end = vec + new Vector3(sprite_size * (j + __col - 1), -sprite_size * (i + __row - 1), 0);
                                var pos = (start + end) / 2f;
                                go.transform.localPosition = pos;
                                go.GetComponent<SpriteRenderer>().size = new Vector2(go.GetComponent<SpriteRenderer>().size.x * __col + 0.02f * __col - 0.02f, 0.675f * __row + 0.02f * __row - 0.02f);
                            }
                            else if (__objectsDic["ROW"] != null)
                            {
                                var start = vec.y - sprite_size * i;
                                var end = vec.y - sprite_size * (i + int.Parse(__objectsDic["ROW"]) - 1);
                                var y = (start + end) / 2f;
                                go.transform.localPosition = new Vector3(sprite_size * j + vec.x, y, 0);
                            }
                            else if (__objectsDic["COL"] != null)
                            {
                                var start = vec.x + sprite_size * j;
                                var end = vec.x + sprite_size * (j + int.Parse(__objectsDic["COL"]) - 1);
                                var x = (start + end) / 2f;
                                go.transform.localPosition = new Vector3(x, -sprite_size * i + vec.y, 0);
                            }
                            else
                            {
                                go.transform.localPosition = vec + new Vector3(sprite_size * j, -sprite_size * i, 0);
                            }
                        }
                        else
                        {
                            //if no properties
                            if( i % 2 == 0)
                            go.transform.localPosition = vec + new Vector3(sprite_size * j * 2, -sprite_size * i * Mathf.Sqrt(3), 0);
                            else
                            go.transform.localPosition = vec + new Vector3(sprite_size * j * 2 + 1, -sprite_size * i * Mathf.Sqrt(3), 0);

                        }
                    }
                }
            }
        }
    }
}