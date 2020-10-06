﻿/*
 * Copyright (c) 2015 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            if ((gameObject.tag == "Donut") || (gameObject.tag == "Pizza") || (gameObject.tag == "Arrow")
             || (gameObject.tag == "Scythe") || (gameObject.tag == "Bone") || (gameObject.tag == "Green"))
            {
                if (GetComponent<Bullet>().bulletProperties.Exists(x => x == "bounce") && GetComponent<Bullet>().numBounces > 0)
                {
                    GetComponent<Bullet>().numBounces--;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                //Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Character" && !other.gameObject.GetComponentInParent<playerMovement>().isRolling)
        {
            if ((gameObject.tag == "Donut") || (gameObject.tag == "Pizza") || (gameObject.tag == "Bone") || (gameObject.tag == "Scythe")
             || (gameObject.tag == "Green"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                // Destroy(gameObject);
            }
        }

    }
}
