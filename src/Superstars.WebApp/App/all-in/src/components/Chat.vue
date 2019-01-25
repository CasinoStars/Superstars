<template>
  <div>
    <launcher :onMessageWasSent="onMessageWasSent" :messageList="messageList" :isOpen="isChatOpen" :close="closeChat"
      :open="openChat" :showEmoji="false" :showFile="false" :colors="colors" :alwaysScrollToBottom="alwaysScrollToBottom" />
  </div>
</template>

<script>
  import launcher from './Chat/Launcher.vue'
  import ChatApiService from '../services/ChatApiService';
  import UserApiService from '../services/UserApiService';
  import WalletApiService from '../services/WalletApiService';

  export default {

    name: 'chat',
    components: {
      launcher
    },
    computed: {
      auth: () => UserApiService
    },
    data() {
      return {
        messageList: [],
        connection: null,
        isChatOpen: false, // to determine whether the chat window should be open or closed
        colors: {
          header: {
            bg: '#373C42',
            text: '#ffffff'
          },
          launcher: {
            bg: '#373C42'
          },
          messageList: {
            bg: '#ffffff'
          },
          sentMessage: {
            bg: '#373C42',
            text: '#ffffff'
          },
          receivedMessage: {
            bg: '#eaeaea',
            text: '#222222'
          },
          userInput: {
            bg: '#373C42',
            text: '#565867'
          }
        }, // specifies the color scheme for the component
        alwaysScrollToBottom: true // when set to true always scrolls the chat to the bottom when new events are in (new message, user starts typing...)
      }
    },
    methods: {
      async onMessageWasSent(message) {
        // called when the user sends a message
        this.messageList = [...this.messageList, message]
        ChatApiService.SendMessage(message.textMessage)
        message = message.textMessage.split(' ');
        console.log(message)
        if (message[0] == "!tip") {
          var model = {
            destinationAccount: message[2],
            amountToSend: message[1]
          }

          if (!await WalletApiService.isPseudoExist(model)) {
              this.messageList = [...this.messageList, {
                userName: "Bot",
                textMessage: message[2] + " n'existe pas."
              }];
            } else if (await WalletApiService.GetTrueBalance() < message[1]) {
              this.messageList = [...this.messageList, {
                userName: "Bot",
                textMessage: "Vous n'avez pas " + message[2] + " sur votre compte."
              }];
            } else {
              await WalletApiService.Transfer(model);
              this.messageList = [...this.messageList, {
                userName: "Bot",
                textMessage: "Vous avez envoyer " + message[1] + " à " + message[2] + "."
              }];



            }

          }

        },
        async openChat() {
            // called when the user clicks on the fab button to open the chat
            this.isChatOpen = true
            this.messageList = await this.GetMessageList();
          },
          closeChat() {
            // called when the user clicks on the botton to close the chat
            this.isChatOpen = false
          },
          async GetMessageList() {
            return await ChatApiService.GetMessageList();
          }
      },
      mounted() {
        var notif = null
        var signalR = require("@aspnet/signalr")
        this.connection = new signalR.HubConnectionBuilder().withUrl("/SignalR")
          .build();
        this.connection.on("Message", (user, message) => {
          if (user != this.auth.pseudo) {
            this.messageList = [...this.messageList, {
              userName: user,
              textMessage: message
            }];
          }
        });
        this.connection.start();
      }
    }
</script>