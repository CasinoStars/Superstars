<template>
    <div id="home">
    <header class="ich" style="padding:128px 16px;">
        <div style="text-align:center; letter-spacing: 1px; font-family: 'Courier New', sans-serif;"> 
            <h1>WELCOME ON ALL`IN</h1>
        </div>
    </header>
    
        <div class="colonne">

            <div class="container">
              <img src="../img/provablylogo.png" class="logo1">
                <div class="text">Un site provably Fair</div>
                <!-- <div class="secondtext"> Grâce à notre système ProvablyFair, <br> vous pouvez avoir confiance en notre Casino !
                </div>                -->
            </div>

            <div class="container">
                 <img src="../img/statslogo.png" class="logo1">
                <div class="text"> Des statistiques détaillées </div>
            </div>

            <div class="container">
            <img src="../img/fastbetlogo.png" class="logo1">
                <div class="text"> Des jeux rapides </div>
            </div>
        </div>
    


    </div>

</template>

<script>
import { mapActions } from 'vuex';
import Vue from 'vue';

export default {

     methods: {
    ...mapActions(['executeAsyncRequest']),
     

     } 
}
</script>

<style lang="css">


.container {
  position: relative;
  width: 0%;
  display: -webkit-inline-block;
}

.ich {
      background-color: rgb(235,235,235)
}

.logo1 {
    width: 170px;
    height: 180px;

}
  
  .colonne {
        display: -webkit-inline-box;
  }

  .secondtext {
    font-size: 20px;
  font-family: 'Courier New', sans-serif;
  font-variant: small-caps;
  color: black;
  }

</style>


  