<template>
    <div id="home">
      <div class="games">
          <div class="yams">
            <router-link v-on:click.native="PlayYams()" to="/playyams">
            <img src="../img/LOGO1.png" alt="yams" id="imgyams">
            </router-link>
            <img src="../img/LOGO2.png" alt="textyams" id="textyams">
          </div>
            <img src="../img/LOGO1.png" alt="poker" id="imgpoker">     
            <img src="../img/LOGO1.png" alt="blackjack" id="imgblackjack">
            <img src="../img/LOGO1.png" alt="game3" id="imggame3">     
            <img src="../img/LOGO1.png" alt="game4" id="imggame4">
      </div>

    </div>

</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import UserApiService from '../services/UserApiService';
import Vue from 'vue';

export default{
  
  computed: {
    ...mapGetters(['isLoading']),
    auth: () => UserApiService
  },
  
  mounted() {
    UserApiService.registerAuthenticatedCallback(() => this.onAuthenticated());
  },

  beforeDestroy() {
    UserApiService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  methods: {

    PlayYams() {
      //
    },
  }
}
</script>

<style>
  .games {
  margin: 290px;
  margin-left: 400px;
  display: -webkit-inline-box;
}

.textgames {
  margin-top: 190px;
  margin-left: 55px;
}

#textyams {
  height: 35px;
  width: 100px;
  transition: transform .4s;
  visibility: hidden;
}


#imgyams {
  transition: transform .4s;
  height: 180px;
  width: 215px;
}

#imgyams:hover{
    transform: scale(1.4);
}

.yams:hover #textyams {
visibility: visible;
}

#imgpoker {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imgpoker:hover {
    transform: scale(1.4); 
}

#imgblackjack {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imgblackjack:hover {
    transform: scale(1.4); 
}

#imggame3 {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imggame3:hover {
    transform: scale(1.4); 
}

#imggame4 {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imggame4:hover {
    transform: scale(1.4); 
}

</style>


  