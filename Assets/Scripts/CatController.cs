using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Cat
{
    void idle(bool toggle);
    void walk(bool toggle);
    void run(bool toggle);
    void jump();
    void dead();
}
