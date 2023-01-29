using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    [SerializeField] private List<IamInTheWay> currentlyInTheWay, alreadyTransparent;
    [SerializeField] private Transform player;
    private Transform m_camera;
    private void Awake() {

        currentlyInTheWay = new List<IamInTheWay>();
        alreadyTransparent = new List<IamInTheWay>();
        m_camera = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetAllObjectsInTheWay();

        MakeObjecctsSolid();
        MakeObjectsTransparent();
    }

    public void GetAllObjectsInTheWay() {

        //calls function each fram so we need to clear
        currentlyInTheWay.Clear();

        float cameraPlayerDistance = Vector3.Magnitude(m_camera.position - player.position);
        Ray ray_from_player = new Ray(m_camera.position, player.position - m_camera.position);
        //Ray ray_from_player = new Ray(player.position, m_camera.position - player.position);


        var hits_from_player = Physics.RaycastAll(ray_from_player, cameraPlayerDistance);

        foreach (var hit in hits_from_player)
        {
            try
            {
                if (hit.collider.gameObject.transform.parent.TryGetComponent(out IamInTheWay inTheWay)) {

                    if (!currentlyInTheWay.Contains(inTheWay)) {

                        currentlyInTheWay.Add(inTheWay);
                    }
                }
            } catch {}
            
        }
    }

    public void MakeObjectsTransparent() {

        for(int i = 0; i < currentlyInTheWay.Count; i++) {

            IamInTheWay inTheWay = currentlyInTheWay[i];

            if (!alreadyTransparent.Contains(inTheWay)) {

                inTheWay.ShowTransparent();
                alreadyTransparent.Add(inTheWay);
            }
        }
    }

    public void MakeObjecctsSolid() {

        for (int i = alreadyTransparent.Count-1; i >= 0; i--) {

            IamInTheWay wasInTheWay = alreadyTransparent[i];

            if (!currentlyInTheWay.Contains(wasInTheWay)) {

                wasInTheWay.ShowSolid();
                alreadyTransparent.Remove(wasInTheWay);
            }
        }
    }
}
