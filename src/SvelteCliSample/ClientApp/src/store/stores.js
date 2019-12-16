import { writable } from 'svelte/store';

export const selectedTests = writable(0);
export const availableTests = writable(0);