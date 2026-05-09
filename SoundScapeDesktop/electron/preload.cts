import { contextBridge, ipcRenderer } from 'electron'

contextBridge.exposeInMainWorld('soundScapeApi', {
  getInputMics: () => ipcRenderer.invoke('devices:get-input-mics'),
  getOutputMics: () => ipcRenderer.invoke('devices:get-output-mics'),

  getAllConfigs: () => ipcRenderer.invoke('config:get-configs'),
  setConfig: () => ipcRenderer.send('config:set-active-config'),
  updateConfig: () => ipcRenderer.send('config:update-config')
})
