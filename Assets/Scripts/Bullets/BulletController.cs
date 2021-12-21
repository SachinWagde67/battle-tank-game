using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletServices
{
    public class BulletController
    {
        public BulletModel bulletModel { get; private set; }
        public BulletView bulletView { get; private set; }
        private Rigidbody rb;

        public BulletController(BulletModel _model, BulletView _bulletView, Vector3 _position, Quaternion _rotation)
        {
            bulletModel = _model;
            bulletView = GameObject.Instantiate<BulletView>(_bulletView, _position, _rotation);
            rb = bulletView.GetComponent<Rigidbody>();
            bulletView.SetBulletController(this);
            bulletModel.SetBulletController(this);
        }

        public void BulletMove()
        {
            Vector3 move = bulletView.transform.position;
            move += bulletView.transform.forward * bulletModel.Speed * Time.fixedDeltaTime;
            rb.MovePosition(move);
        }
    }
}
