# AutoFib Locked with Hotkey

<img width="891" height="886" alt="cT_cs_1184089_XAUUSD_2026-02-14_21-33-36" src="https://github.com/user-attachments/assets/5d419a8c-e200-430a-b3fe-b29a8aaa1cb2" />

Indikator Fibonacci otomatis untuk cTrader dengan klik mouse untuk menentukan level harga, arah Fibonacci (Low→High / High→Low), dan hotkey yang bisa dikustomisasi.

## Fitur

- **Set Low**: Klik kiri biasa di chart
- **Set High**: Ctrl + Klik kiri di chart
- **Toggle Lock**: Tekan hotkey Lock (default: **L**)
- **Toggle Direction**: Tekan hotkey Direction (default: **D**)
  - `LowToHigh` → 0% di Low, 100% di High
  - `HighToLow` → 0% di High, 100% di Low
- Saat terkunci:
  - Klik mouse diabaikan
  - Level tidak ikut update higher high / lower low baru
  - Warna garis menjadi lebih gelap
- Hotkey bisa diganti lewat parameter (tidak perlu edit kode)
- Status hotkey & lock ditampilkan langsung di chart (pojok kiri atas)
  - Hijau → hotkey aktif
  - Oranye → ada hotkey yang gagal (sudah dipakai indikator/cBot lain)
- Custom level Fibonacci (maksimal 5 level)
  - Bisa aktifkan/matikan per level
  - Nilai level bebas (retracement, extension, negatif)
  - Warna per level via color picker
- Label menampilkan persentase + harga (contoh: `61.8% (1.23456)`)
- Nilai Low/High bertahan meski ubah parameter atau toggle lock

<img width="699" height="860" alt="parameter-autofib" src="https://github.com/user-attachments/assets/b4351388-e32b-44b7-997d-409905bc5f54" />

## Cara Pakai

1. Attach indikator ke chart
2. Klik kiri → set **Low**
3. Ctrl + Klik kiri → set **High**  
   → Garis Fibonacci muncul otomatis
4. Tekan **L** (default) → kunci level
5. Tekan **L** lagi → buka kunci
6. Tekan **D** (default) → ganti arah Fibonacci (LowToHigh ↔ HighToLow)
7. Ubah warna, style, thickness, custom level, atau hotkey di parameter
8. Untuk reset total: remove indikator dari chart, lalu attach ulang

## Parameter Utama

| Group | Parameter | Keterangan |
|-------|-----------|------------|
| **Settings** | Direction | Arah awal Fibonacci (`LowToHigh` / `HighToLow`) |
| **Hotkeys** | Lock Hotkey | Tombol untuk lock/unlock (contoh: `L`, `K`, `X`) |
| **Hotkeys** | Direction Hotkey | Tombol untuk ganti arah (contoh: `D`) |
| **Colors** | High / 50% / Low Color | Warna saat tidak terkunci |
| **Appearance** | Line Style & Thickness | Gaya dan ketebalan garis |
| **Labels** | Show Level Labels | Tampilkan label % + harga |
| **Custom Levels** | Level 1–5 | Enable, value, dan warna masing-masing |

## Catatan

- Garis Low (0%) dan High (100%) selalu aktif
- Level custom bisa overlap (misal 50% di custom level 4)
- Jika hotkey gagal ditambahkan (sudah dipakai indikator/cBot lain), peringatan oranye akan muncul di chart
- Ganti hotkey di parameter jika terjadi konflik
- Nilai hotkey yang valid: `A`–`Z`, `D0`–`D9`, `F1`–`F12`, `Space`, dll (sesuai enum `Key` cAlgo)

Build di cTrader Automate → attach → selesai.
