using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsNewCarReleases
    {
        
            public event EventHandler<clsCar> NewCarReleasesed;
            
            public void CreateCar(clsCar NewCar)
            {

                OnCarCreted(NewCar);
            }

            protected virtual void OnCarCreted(clsCar e)
            {
                NewCarReleasesed?.Invoke(this, e);
            }
        }





    }


