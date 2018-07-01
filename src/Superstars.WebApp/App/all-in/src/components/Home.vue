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
              <div @mouseover="Showtextlogo1()" @mouseleave="Hidetextlogo1()" class="overlay">
                <div class="text">Un site provably Fair</div>
              </div>
            <div id="DIV1" class="mydiv">
                A provably fair site
            </div>
            </div>
            <div class="container">
                 <img src="../img/statslogo.png" class="logo1">
                 <div @mouseover="Showtextlogo2()" @mouseleave="Hidetextlogo2()" class="overlay">
                <div class="text"> Des statistiques détaillées </div>
              </div>
            <div id="DIV2" class="mydiv">
                Des statistiques pour toutes vos parties
            </div>
            </div>
            <div class="container">
            <img src="../img/fastbetlogo.png" class="logo1">
              <div @mouseover="Showtextlogo3()" @mouseleave="Hidetextlogo3()" class="overlay">
                <div class="text"> Des jeux rapides </div>
              </div>
            <div id="DIV3" class="mydiv">
                Des jeux rapides
            </div>
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
     
     Showtextlogo1() {
        var x = document.getElementById("DIV1");
        x.style.display = "block";
},

     Hidetextlogo1() {
        var x = document.getElementById("DIV1");
        x.style.display = "none";
},

     Showtextlogo2() {
        var x = document.getElementById("DIV2");
        x.style.display = "block";
},
     Hidetextlogo2() {
        var x = document.getElementById("DIV2");
        x.style.display = "none";
},

     Showtextlogo3() {
        var x = document.getElementById("DIV3");
        x.style.display = "block";
},

     Hidetextlogo3() {
        var x = document.getElementById("DIV3");
        x.style.display = "none";
},


     } 
}
</script>

<style lang="css">

#DIV1 {
    width: 220px;
    padding: 20px 0;
    text-align: center;
    background-color: rgb(235,235,235);
    margin-top: 5px;
    display: none;
}

#DIV2 {
    width: 220px;
    padding: 20px 0;
    text-align: center;
    background-color: rgb(235,235,235);
    margin-top: 5px;
    display: none;
}

#DIV3 {
    width: 220px;
    padding: 20px 0;
    text-align: center;
    background-color: rgb(235,235,235);
    margin-top: 10px;
    display: none;
}

.container {
  position: relative;
  width: 0%;
  display: -webkit-inline-block;
  
}

.container:hover .overlay {
  opacity: 0.65;
}

.text {
  color: black;
  font-size: 25px;
  position: relative;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  text-align: center;
  font-family: 'Courier New', sans-serif;
}

.overlay {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  height: 180px;
  width: 650px;
  opacity: 0;
  transition: .5s ease;
  background-color: rgb(5,5,5);
}

  .ich {
      background-color: rgb(235,235,235)
  }

  .logo1 {
      width: 170px;
      height: 180px;
      position: relative;
  }

  .colonne {
        display: -webkit-inline-box;
  }

</style>


  