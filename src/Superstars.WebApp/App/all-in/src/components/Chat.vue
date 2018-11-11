<template>
    <div>
        <launcher
            :onMessageWasSent="onMessageWasSent"
            :messageList="messageList"
            :isOpen="isChatOpen"
            :close="closeChat"
            :open="openChat"
            :showEmoji="false"
            :showFile="false"
            :colors="colors"
            :alwaysScrollToBottom="alwaysScrollToBottom" />
    </div>
</template>

<script>
import launcher from './Chat/Launcher.vue'
import ChatApiService from '../services/ChatApiService';

export default {
    
    name: 'chat',
    components: {
        launcher
    },
     data() {
    return {
      messageList: [],
      isChatOpen: false, // to determine whether the chat window should be open or closed
      colors: {
        header: {
          bg: '#4e8cff',
          text: '#ffffff'
        },
        launcher: {
          bg: '#4e8cff'
        },
        messageList: {
          bg: '#ffffff'
        },
        sentMessage: {
          bg: '#4e8cff',
          text: '#ffffff'
        },
        receivedMessage: {
          bg: '#eaeaea',
          text: '#222222'
        },
        userInput: {
          bg: '#f4f7f9',
          text: '#565867'
        }
      }, // specifies the color scheme for the component
      alwaysScrollToBottom: true // when set to true always scrolls the chat to the bottom when new events are in (new message, user starts typing...)
    }
  },
  methods: {
    onMessageWasSent (message) {
      // called when the user sends a message
      this.messageList = [ ...this.messageList, message ]
      ChatApiService.SendMessage(message.textMessage)

    },
    async openChat () {
      // called when the user clicks on the fab button to open the chat
      this.isChatOpen = true
      this.messageList = await this.GetMessageList();
    },
    closeChat () {
      // called when the user clicks on the botton to close the chat
      this.isChatOpen = false
    },
    async GetMessageList(){
      return await ChatApiService.GetMessageList();
    }
  }
}
</script>
