using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace maker {

    public class ObjectInfo
    {
        public class Action {
            public string   Name { get; set;}
            public string   Asset { get; set; }
            public int      Rows { get; set; }
            public int      Columns { get; set; }
            public float    Scale { get; set; }

            public Action() {
            }
        }

        public string       Type { get; set; }
        public string       Name { get; set; }
        public List<Action> Actions { get; set; }
        public Objekt       Obj { get; set; }

        public ObjectInfo () {
        }
       
    }

    public class Level {
        public string Name { get; set; }
        public List<ObjectInfo> Objects;

        public Level () {
        }

        public Level (string name) {
            Name = name;
        }

        public Dictionary<string, Objekt> Load (MacGame game,
                                               ContentManager content) {
            Dictionary<string, Objekt> levelObjects = 
                new Dictionary<string, Objekt> ();

            Objekt add = null;

            foreach (ObjectInfo o in Objects) {
                switch(o.Type)
                {
                case "maker.Objekt":
                    add = new Objekt(game,o.Obj.Collidable);
                    break;
                case "maker.Tile":
                    Tile tile = (Tile)o.Obj;
                    add = new Tile(game,true);
                    //((Tile)add).SolidBottom = tile.SolidBottom;
                    //((Tile)add).SolidLeft = tile.SolidLeft;
                    //((Tile)add).SolidRight = tile.SolidRight;
                    //((Tile)add).SolidTop = tile.SolidTop;
                    break;
                case "maker.Player":
                    Player player = (Player)o.Obj;
                    add = new Player(game);
                    break;
                }

                add.Position = o.Obj.Position;
                foreach(ObjectInfo.Action a in o.Actions)
                {
                    add.AddSprite(a.Name, new Sprite(content,a.Asset));
                }

                levelObjects.Add(o.Name,add);
            }

            return levelObjects;
        }

        public void Save (Dictionary<string, Objekt> _objekts) {
            Objects = new List<ObjectInfo>();

            ObjectInfo temp; 
            foreach(KeyValuePair<string, Objekt> kvp in _objekts){
                temp = new ObjectInfo();
                temp.Name = kvp.Key;
                temp.Type = kvp.Value.GetType().ToString();
                temp.Obj = kvp.Value;
                temp.Actions = new List<ObjectInfo.Action>();
                ObjectInfo.Action newAction;

                foreach(KeyValuePair<string, Sprite> sp in kvp.Value.Sprites){
                    newAction = new ObjectInfo.Action();
                    newAction.Name = sp.Key;
                    newAction.Asset = sp.Value.Texture.Name;
                    newAction.Columns = sp.Value.Columns;
                    newAction.Rows = sp.Value.Rows;
                    newAction.Scale = sp.Value.Scale;
                    temp.Actions.Add(newAction);
                }

                Objects.Add(temp);
            }
        }
    }

    public static class LevelLoader
    {
        public static void Save(Level level)
        {

        }

        private static void LoadActions (Objekt o, JArray actions, MacGame game) {
            //JArray actions = (JArray)obj["Actions"];
            string firstAction = (string)actions[0]["Name"];
            int rows = 0;
            int cols = 0;
           
            for(int i2 = 0; i2 < actions.Count; i2++)
            {
                rows = (int)actions[i2]["Rows"];
                cols = (int)actions[i2]["Columns"];
                //scale = (float)actions[i2]["Scale"];
                if(rows == 0 && cols == 0)
                {
                    o.AddSprite((string)actions[i2]["Name"],
                                  new Sprite(game.Content, (string)actions[i2]["Asset"]));
                }
                else
                {
                    o.AddSprite((string)actions[i2]["Name"],
                                  new Sprite(game.Content, (string)actions[i2]["Asset"],rows,cols));
                }
            }

            o.SelectedAction = firstAction;

        }

        public static Dictionary<string, Objekt> Load (string fileName,
                                                       MacGame game,
                                                       ContentManager content) {

            Dictionary<string, Objekt> gameObjekts = new Dictionary<string, Objekt>();

            string levelFile = File.ReadAllText (@"/Users/Fritz/Documents/level_Save.json");
            JObject l = JObject.Parse (levelFile);

            Level level = new Level ((string)l ["Name"]);
            JArray objekts = (JArray)l ["Objects"];
            Objekt o = null;
            for (int i = 0; i < objekts.Count; i++) {
                JObject obj = (JObject)objekts[i];
                Objekt add = null;
                JObject gObject = (JObject)obj["Obj"];
                JArray actions = (JArray)obj["Actions"];

                switch((string)obj["Type"])
                {
                    case "maker.Objekt":
                        add = new Objekt(game,false);
                        LoadActions(add, actions, game);
                        break;
                    case "maker.Tile":
                        add = new Tile(game,true);
                        ((Tile)add).SolidBottom = (bool)gObject["SolidBottom"];
                        ((Tile)add).SolidLeft = (bool)gObject["SolidLeft"];
                        ((Tile)add).SolidRight = (bool)gObject["SolidRight"];
                        ((Tile)add).SolidTop = (bool)gObject["SolidTop"];
                        LoadActions(add, actions, game);
                        break;
                    case "maker.Player":
                        add = new Player(game);
                        break;
                    case "maker.MousePointer":
                        add = new MousePointer(game);
                        break;
                }

                //JObject gObject = (JObject)obj["Obj"];
                bool collidable = (bool)gObject["Collidable"];
                JObject position = (JObject)gObject["Position"];
                add.Position = new Vector2((float)position["X"],
                                           (float)position["Y"]);

                string firstAction = (string)actions[0]["Name"];
                float scale = (float)actions[0]["Scale"];

                add.Scale = scale;
                add.Name = (string)obj["Name"];
                gameObjekts.Add((string)obj["Name"], add);
            }


            return gameObjekts;
        }
    }
}

