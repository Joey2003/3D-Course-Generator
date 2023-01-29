using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionChooser : MonoBehaviour
{
    public static int NORTH = 1;
    public static int EAST = 2;
    public static int SOUTH = 3;
    public static int WEST = 4;
    public static int UP = 5;
    public static int DOWN = 6;

    public MapSection
    nu_wj,eu_wj,su_wj,wu_wj,
    un_wj,ue_wj,us_wj,uw_wj,
    uu_wj_1;
    MapSection[] sections;
    public MapSection
    nn,ne,nw,nu,nd,
    en,ee,es,eu,ed,
    se,ss,sw,su,sd,
    wn,ws,ww,wu,wd,
    un,ue,us,uw,//uu,
    dn,de,ds,dw,dd;



    public GameObject
    _nn,_ne,_nw,_nu,_nd,
    _en,_ee,_es,_eu,_ed,
    _se,_ss,_sw,_su,_sd,
    _wn,_ws,_ww,_wu,_wd,
    _un,_ue,_us,_uw,_uu,
    _dn,_de,_ds,_dw,_dd,
    _nu_wj,_eu_wj,_su_wj,_wu_wj,
    _un_wj,_ue_wj,_us_wj,_uw_wj,
    _uu_wj_1;
    
    // Start is called before the first frame update
    void Start()
    {
        nn = new MapSection(_nn, NORTH, NORTH, false, -1);
        ne = new MapSection(_ne, NORTH, EAST, false, -1);
        //ns = new MapSectio, n(NORTH, SOUTH, false, -1);
        nw = new MapSection(_nw, NORTH, WEST, false, -1);
        nu = new MapSection(_nu, NORTH, UP, false, -1);
        nd = new MapSection(_nd, NORTH, DOWN, false, -1);
        en = new MapSection(_en, EAST, NORTH, false, -1);
        ee = new MapSection(_ee, EAST, EAST, false, -1);
        es = new MapSection(_es, EAST, SOUTH, false, -1);
        //ew = new MapSectio, n(EAST, WEST, false, -1);
        eu = new MapSection(_eu, EAST, UP, false, -1);
        ed = new MapSection(_ed, EAST, DOWN, false, -1);
        //sn = new MapSectio, n(SOUTH, NORTH, false, -1);
        se = new MapSection(_se, SOUTH, EAST, false, -1);
        ss = new MapSection(_ss, SOUTH, SOUTH, false, -1);
        sw = new MapSection(_sw, SOUTH, WEST, false, -1);
        su = new MapSection(_su, SOUTH, UP, false, -1);
        sd = new MapSection(_sd, SOUTH, DOWN, false, -1);
        wn = new MapSection(_wn, WEST, NORTH, false, -1);
        //we = new MapSectio, n(WEST, EAST, false, -1);
        ws = new MapSection(_ws, WEST, SOUTH, false, -1);
        ww = new MapSection(_ww, WEST, WEST, false, -1);
        wu = new MapSection(_wu, WEST, UP, false, -1);
        wd = new MapSection(_wd, WEST, DOWN, false, -1);
        un = new MapSection(_un, UP, NORTH, false, -1);
        ue = new MapSection(_ue, UP, EAST, false, -1);
        us = new MapSection(_us, UP, SOUTH, false, -1);
        uw = new MapSection(_uw, UP, WEST, false, -1);
        //uu = new MapSection(_uu, UP, UP, false, -1);
        //ud = new MapSectio, n(UP, DOWN, false, -1);
        dn = new MapSection(_dn, DOWN, NORTH, false, -1);
        de = new MapSection(_de, DOWN, EAST, false, -1);
        ds = new MapSection(_ds, DOWN, SOUTH, false, -1);
        dw = new MapSection(_dw, DOWN, WEST, false, -1);
        //du = new MapSectio, n(DOWN, UP, false, -1);
        dd = new MapSection(_dd, DOWN, DOWN, false, -1);

        ///WALL JUMP SECTIONS///
        nu_wj = new MapSection(_nu_wj, NORTH, UP, true, -1);
        eu_wj = new MapSection(_eu_wj, EAST, UP, true, -1);
        su_wj = new MapSection(_su_wj, SOUTH, UP, true, -1);
        wu_wj = new MapSection(_wu_wj, WEST, UP, true, -1);
        un_wj = new MapSection(_un_wj, UP, NORTH, true, -1);
        ue_wj = new MapSection(_ue_wj, UP, EAST, true, -1);
        us_wj = new MapSection(_us_wj, UP, SOUTH, true, -1);
        uw_wj = new MapSection(_uw_wj, UP, WEST, true, -1);
        uu_wj_1 = new MapSection(_uu_wj_1, UP, UP, true, 1);

        sections = new MapSection [38] {nn, ne, nw, nu, nd, 
                                        en, ee, es, eu, ed, 
                                        se, ss, sw, su, sd, 
                                        wn, ws, ww, wu, wd, 
                                        un, ue, us, uw, //uu, 
                                        dn, de, ds, dw, dd, 
                                        nu_wj, eu_wj, su_wj, 
                                        wu_wj, un_wj, ue_wj, 
                                        us_wj, uw_wj, 
                                        uu_wj_1 };
    }



    public class MapSection {

        public int entrance_direction, exit_direction, x, y, z;
        public int part;
        public bool wall_jump;
        public GameObject gameObject;

        public MapSection(int entrance_direction, int exit_direction, bool wall_jump, int part) {

            this.entrance_direction = entrance_direction;
            this.exit_direction = exit_direction;
            this.wall_jump = wall_jump;
            this.part = part;

        }
        public MapSection(GameObject prefab, int entrance_direction, int exit_direction, bool wall_jump, int part) {

            this.entrance_direction = entrance_direction;
            this.exit_direction = exit_direction;
            this.wall_jump = wall_jump;
            this.gameObject = prefab;
            this.part = part;
        }
        
        public void setCoords(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;

        }

        public string getCoords() {
            return x + "," + y + "," + z;

        }

        public void setGameObject(GameObject obj) {
            this.gameObject = obj;
        }

        public void setDirections(int entrance, int exit) {
            this.entrance_direction = entrance;
            this.exit_direction = exit;
        }
    }
        //bool execute = true;

    MapSection nullSection;
    public MapSection getValidSection(MapSection previousSec, SectionMatrix matrix) {

        nullSection = new MapSection(-1, -1, false, -1);
    
        MapSection[] validSections = new MapSection[39] {nullSection, nullSection, nullSection, nullSection, nullSection,
                                                         nullSection, nullSection, nullSection, nullSection, nullSection, 
                                                         nullSection, nullSection, nullSection, nullSection, nullSection, 
                                                         nullSection, nullSection, nullSection, nullSection, nullSection, 
                                                         nullSection, nullSection, nullSection, nullSection, nullSection, 
                                                         nullSection, nullSection, nullSection, nullSection, nullSection,
                                                         nullSection, nullSection, nullSection, nullSection, nullSection,
                                                         nullSection, nullSection, nullSection, nullSection};
        System.Random rnd = new System.Random();

        int count = 0;
        foreach (MapSection sec in sections) {
            int x = 0;
            int y = 0;
            int z = 0;

            int prev = 0;


            if (sec.entrance_direction == previousSec.exit_direction) {

                //checks if section is a tunnel leading to edge of matrix
                if(((sec.exit_direction == NORTH ) &&  previousSec.z + 1 == matrix.matrixSize-1) == false
                && ((sec.exit_direction == EAST ) &&  previousSec.x + 1 == matrix.matrixSize-1) == false
                && ((sec.exit_direction == SOUTH ) &&  previousSec.z - 1 == 0) == false
                && ((sec.exit_direction == WEST ) &&  previousSec.x - 1 == 0) == false
                && ((sec.exit_direction == UP ) &&  previousSec.y - 1 == 0) == false
                && ((sec.exit_direction == DOWN ) &&  previousSec.y + 1 == matrix.matrixSize-1) == false) {

                    if ((!sec.wall_jump && previousSec.part == 1) == false
                    && (!sec.wall_jump && (previousSec.exit_direction == UP && previousSec.wall_jump)) == false
                    && ((sec.wall_jump && sec.part == 1) && !previousSec.wall_jump) == false
                    && ((sec.wall_jump && sec.part == 1) && !(previousSec.part == -1 && previousSec.wall_jump) && !(previousSec.part == 1 && previousSec.wall_jump)) == false
                    && ((sec.wall_jump && sec.part == -1 && sec.exit_direction == UP) && (previousSec.wall_jump)) == false
                    && ((sec.wall_jump && sec.part == -1 && sec.exit_direction != UP) && (previousSec.part == -1)) == false) {
                  /*  if(sec.wall_jump)
                    if ((sec.wall_jump && (!previousSec.wall_jump && sec.part == -1))
                    || ((sec.wall_jump && sec.part == 1) && (previousSec.wall_jump && previousSec.part == -1))
                    || ((sec.wall_jump && sec.part == 2) && (previousSec.wall_jump && previousSec.part == 1))
                    || (sec.wall_jump && sec.part == -1) && (previousSec.wall_jump && previousSec.part == 1))
*/
                    
                        //2 units per peice to help with deadend detection 
                        if(sec.entrance_direction == NORTH) {z++;}
                        else if(sec.entrance_direction == EAST) {x++;}
                        else if(sec.entrance_direction == SOUTH) {z--;}
                        else if(sec.entrance_direction == WEST) {x--;}
                        else if(sec.entrance_direction == UP) {y--;}
                        else if(sec.entrance_direction == DOWN) {y++;}

                        if(sec.exit_direction == NORTH) {z++;}
                        else if(sec.exit_direction == EAST) {x++;}
                        else if(sec.exit_direction == SOUTH) {z--;}
                        else if(sec.exit_direction == WEST) {x--;}
                        else if(sec.exit_direction == UP) {y--;}
                        else if(sec.exit_direction == DOWN) {y++;}

                        //if the matrix spot is not filled, add the section to a list of valid sections
                        int index_x = previousSec.x + x;
                        int index_y = previousSec.y + y;
                        int index_z = previousSec.z + z;
        //                 print(""+index_x+" "+ index_y+" "+index_z);
                        if(index_x >= 0 && index_x < matrix.matrixSize
                        && index_y >= 0 && index_y < matrix.matrixSize 
                        && index_z >= 0 && index_z < matrix.matrixSize) {

                        
                            if (!matrix.matrix[index_y, index_x, index_z]) {


                                if (count == 1) {
                                    prev = count - 1;
                                    while(validSections[prev] == nullSection) {
                                        count--;
                                        prev = count - 1;

                                        if(count <= 0) {
                                            break;
                                        }
                                    }
                                }

                                validSections[count] = sec;
                                count++;
                            }
                        }
                    }
                }
            }
        }
        
        if (count != 0) {
            return validSections[rnd.Next(count)];
        } else {
            return null;
        }
    }
}
