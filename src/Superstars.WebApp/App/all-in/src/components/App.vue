<template>
  <div id="app">
    <header>
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <router-link class="navbar-brand" to="/" style="font-family: 'Courier New', sans-serif; font-weight: 600px;">ALL`IN</router-link>
        
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        
        <ul class="nav navbar-nav">
          <li class="nav-item">
            <router-link class="nav-link" to="/playersStats" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-trophy" style="font-size: 1.4rem;"></i> CLASSEMENT
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/rule" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-info-circle" style="font-size: 1.4rem;"></i> RÈGLES
            </router-link>
          </li>
          <li class="nav-item" v-if="auth.isConnected">
            <router-link class="nav-link" to="/statistics" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-bar-chart" style="font-size: 1.4rem;"></i> STATISTIQUES
            </router-link>
          </li>
          <li class="nav-item" v-if="auth.isConnected">
              <router-link class="nav-link" to="/ProvablyFair" style="letter-spacing: 2px; font-size: 12px;">
              <i class="fa fa-balance-scale" style="font-size: 1.4rem;"></i> PROVABLYFAIR
              </router-link>
          </li>
        </ul>

        <div class="collapse navbar-collapse" v-if="auth.isConnected">
          <ul class="navbar-nav ml-auto">
            <li class="nav-item" v-if="auth.isConnected">
              <router-link class="nav-link" to="/play" style="letter-spacing: 2px; font-size: 12px;">
                <i class="fa fa-gamepad" style="font-size: 1.4rem;"></i> JOUER
              </router-link>
            </li>
          </ul>
          <ul class="navbar-nav ml-auto">
              <li class="nav-item">
              <router-link class="nav-link" to="/wallet" id="borderSolde" style="letter-spacing: 2px; font-size: 12px;">
                SOLDE DU COMPTE : {{BTCMoney.toLocaleString('en')}}<i class="fa fa-btc" style="font-size: 0.8rem;"></i> || {{fakeMoney.toLocaleString('en')}}<i class="fa fa-money" style="font-size: 0.8rem;"></i>
              </router-link>
            </li>
          </ul>
          <ul class="navbar-nav ml-auto">
            <li class="nav-item dropdown" style="text-transform: uppercase; letter-spacing: 1px; font-size: 12px;">
              <a class="nav-link dropdown-toggle" href="#" id="basic-nav-dropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fa fa-user" style="font-size: 1.4rem;"></i> {{ auth.pseudo }}
              </a>
              <div role="menu" class="dropdown-menu dropdown-menu-right" aria-labelledby="basic-nav-dropdown" style="top:120%;">
                <router-link style="margin-left: -2px;" class="dropdown-item" to="/wallet"><i class="fa fa-diamond"> Porte-feuille</i></router-link>
                <router-link class="dropdown-item" to="" data-toggle="modal" data-target="#myModal"><i class="fa fa-cog"> Régagles</i></router-link>
                <div class="dropdown-divider"></div>
                <router-link class="dropdown-item" to="/logout"><i class="fa fa-sign-out"> Déconnexion</i></router-link>
              </div>
            </li>
          </ul>
        </div>
        
        <div class="collapse navbar-collapse" v-else>
          <ul class="navbar-nav ml-auto">
            <li class="nav-item">
              <router-link class="nav-link" v-on:click.native="log('Login')" to="/#" style="letter-spacing: 2px; font-size: 12px;"><i class="fa fa-sign-in" style="font-size: 1.4rem;"></i> CONNEXION</router-link>
            </li>
            <!-- <li class="nav-item">
              <router-link class="nav-link" v-on:click.native="log('Register')" to="/">INSCRIPTION</router-link>
            </li> -->
          </ul>
        </div>
      </nav>
      <div class="progress" v-if="isLoading">
        <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%"></div>
      </div>
    </header>
    <router-view></router-view>
    <chat v-if="auth.isConnected"></chat>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Gestion de compte</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <ul class="tab-group" v-if="!editMail && !editPassword">
              <li class="tab"><a v-on:click="editPassword=true">Mot De Passe</a></li>
              <li class="tab"><a v-on:click="editMail=true">Adresse Email</a></li>
            </ul>
            <form v-else-if="editPassword">
              <div class="field-wrap">
                <label>
                  Mot de Passe Actuel<span class="req">*</span>
                </label><br><br>
                <input v-model="oldPass" required/>
              </div>
              <div class="field-wrap">
                <label>
                  Nouveau Mot de Passe<span class="req">*</span>
                </label><br><br>
                <input v-model="newPass" required/>
              </div><br>
              <button type="submit" class="btn btn-block">Sauvegarder</button>
            </form>
            <form v-else>email

            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script src="~/lib/signalr/signalr.js"></script>


<script>
import { mapGetters, mapActions } from 'vuex';
import UserApiService from '../services/UserApiService';
import WalletApiService from '../services/WalletApiService';
import Vue from 'vue';
import Chat from './Chat.vue'

export default{
  data() {
    return {
      editPassword: false,
      editMail: false,
      errors: []
    }
  },
  components: {
    Chat
  },
  computed: {
    ...mapGetters(['isLoading']),
    ...mapGetters(['BTCMoney']),
    ...mapGetters(['fakeMoney']),
    auth: () => UserApiService
  },
  
  async mounted() {
    if(UserApiService.isConnected) {
      await this.RefreshBTC();
      await this.RefreshFakeCoins();
    }
    
    UserApiService.registerAuthenticatedCallback(() => this.onAuthenticated());
  },

  beforeDestroy() {
    UserApiService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  methods: {
    ...mapActions(['RefreshFakeCoins']),
    ...mapActions(['RefreshBTC']),
    
    log(selectedBase) {
      UserApiService.log(selectedBase);
    },
    
    async onAuthenticated() {
      await this.RefreshBTC();
      await this.RefreshFakeCoins();
      this.$router.replace('/play');
    },

    showSetPassword(){
      this.showSetPasswords = true;
    },

    showSetMail() {
      this.showSetMail = true
    }
  }
}
</script>

<style lang="scss" scoped>

.app {
    background-color: black;
}

#borderSolde{
  border-style:solid; 
  border-width:0.7px;
  border-color: rgb(74, 80, 180);
}

#borderSolde:hover{
  border-color: rgb(54, 114, 5);
}

.progress {
  margin: 0px;
  padding: 0px;
  height: 5px;
}

a.router-link-active {
  font-weight: bold;
}

</style>

<style lang="scss">
@import "../styles/global.scss";
</style>