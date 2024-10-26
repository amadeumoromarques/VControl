import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  private encryptionKey = 'D4E819A2-5B7A-4ED2-A7C9-8C4317B0D562-ABD1C89D-9E4F-4311-9F3A-2B867CDA1F9E'; // Necessita uma chave robusta

  constructor() { }

  // Salva um valor no localStorage
  setItem(key: string, value: any): void {
    const encryptedValue = CryptoJS.AES.encrypt(JSON.stringify(value), this.encryptionKey).toString();
    localStorage.setItem(key, encryptedValue);
  }

  // Obtém um valor do localStorage
  getItem(key: string): any {
    const item = localStorage.getItem(key);
    if (!item) return null;

    try {
      const decryptedBytes = CryptoJS.AES.decrypt(item, this.encryptionKey);
      const decryptedValue = decryptedBytes.toString(CryptoJS.enc.Utf8);
      return JSON.parse(decryptedValue);
    } catch (e) {
      console.error('Erro ao descriptografar:', e);
      return null;
    }
  }

  // Remove um item do localStorage
  removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  // Limpa todo o localStorage
  clear(): void {
    localStorage.clear();
  }
}
