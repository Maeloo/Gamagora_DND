﻿/*
 * Copyright (c) 2015 Allan Pichardo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System;

/*
 * Make your class implement the interface AudioProcessor.AudioCallbaks
 */
public class AudioManager : MonoBehaviour, AudioProcessor.AudioCallbacks
{
    private AudioProcessor processor;
    private EnemyManager enemiesSpawner;
    private Vector3 currentPos;

    void Awake()
    {
        processor = FindObjectOfType<AudioProcessor>();
        processor.addAudioCallback(this);

        enemiesSpawner = EnemyManager.Instance;
        currentPos = Camera.main.transform.position;
    }

    
    void Update()
    {
        
    }

    // This event will be called every time a beat is detected.
    // Change the threshold parameter in the inspector to adjust the sensitivity
    public void onOnbeatDetected()
    {
        processor.changeCameraColor();
    }

    // This event will be called every frame while music is playing
    public void onData(float[] spectrum, float[] data, float[] data2, float[] data3)
    {
        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i * 0.015f, -1f, 0);
            Vector3 end = new Vector3(i * 0.015f, -1f + 20f * spectrum[i], 0);
            Debug.DrawLine(start, end, Color.yellow);
        }

        for (int i = 0; i < data.Length; ++i)
        {
            Vector3 start = new Vector3(i * 0.065f, 2f, 0);
            Vector3 end = new Vector3(i * 0.065f, 2f + 50000f * data[i], 0);
            Debug.DrawLine(start, end, Color.red);
        }

        for (int i = 0; i < data2.Length; ++i)
        {
            Vector3 start = new Vector3(i * 0.065f, 0.5f, 0);
            Vector3 end = new Vector3(i * 0.065f, 0.5f + 50000f * data2[i], 0);
            Debug.DrawLine(start, end, Color.red);
        }

        for (int i = 0; i < data3.Length; ++i)
        {
            Vector3 start = new Vector3(i * 0.065f, -3f, 0);
            Vector3 end = new Vector3(i * 0.065f, -3f + 5000000000f * data3[i], 0);
            Debug.DrawLine(start, end, Color.yellow);
        }
    }
}
