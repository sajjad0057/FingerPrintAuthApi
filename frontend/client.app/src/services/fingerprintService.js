// src/services/fingerprintService.js

import FingerprintJS from '@fingerprintjs/fingerprintjs';

export const getFingerprint = async () => {
  // Initialize FingerprintJS
  const fp = await FingerprintJS.load();
  // Get the fingerprint
  const result = await fp.get();
  return result.visitorId; // This is the unique fingerprint
};
