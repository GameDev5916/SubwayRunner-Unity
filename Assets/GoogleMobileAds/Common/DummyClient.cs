// Copyright (C) 2015 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Reflection;

using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Common
{
    public class DummyClient : IBannerClient, IInterstitialClient, IRewardBasedVideoAdClient,
            IAdLoaderClient, IMobileAdsClient
    {
        public DummyClient()
        {
            Dummy();
        }

        private void Dummy()
        {
#if UNITY_ANDROID || UNITY_IOS
            Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
#endif
        }

        // Disable warnings for unused dummy ad events.
#pragma warning disable 67

        public event EventHandler<EventArgs> OnAdLoaded;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public event EventHandler<EventArgs> OnAdOpening;

        public event EventHandler<EventArgs> OnAdStarted;

        public event EventHandler<EventArgs> OnAdClosed;

        public event EventHandler<Reward> OnAdRewarded;

        public event EventHandler<EventArgs> OnAdLeavingApplication;

        public event EventHandler<EventArgs> OnAdCompleted;

        public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

#pragma warning restore 67

        public string UserId
        {
            get
            {
                Dummy();
                return "UserId";
            }

            set
            {
                Dummy();
            }
        }

        public void Initialize(string appId)
        {
            Dummy();
        }

        public void SetApplicationMuted(bool muted)
        {
            Dummy();
        }

        public void SetApplicationVolume(float volume)
        {
            Dummy();
        }

        public void SetiOSAppPauseOnBackground(bool pause)
        {
            Dummy();
        }

        public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
        {
            Dummy();
        }

        public void CreateBannerView(string adUnitId, AdSize adSize, int positionX, int positionY)
        {
            Dummy();
        }

        public void LoadAd(AdRequest request)
        {
            Dummy();
        }

        public void ShowBannerView()
        {
            Dummy();
        }

        public void HideBannerView()
        {
            Dummy();
        }

        public void DestroyBannerView()
        {
            Dummy();
        }

        public float GetHeightInPixels()
        {
            Dummy();
            return 0;
        }

        public float GetWidthInPixels()
        {
            Dummy();
            return 0;
        }

        public void SetPosition(AdPosition adPosition)
        {
            Dummy();
        }

        public void SetPosition(int x, int y)
        {
            Dummy();
        }

        public void CreateInterstitialAd(string adUnitId)
        {
            Dummy();
        }

        public bool IsLoaded()
        {
            Dummy();
            return true;
        }

        public void ShowInterstitial()
        {
            Dummy();
        }

        public void DestroyInterstitial()
        {
            Dummy();
        }

        public void CreateRewardBasedVideoAd()
        {
            Dummy();
        }

        public void SetUserId(string userId)
        {
            Dummy();
        }

        public void LoadAd(AdRequest request, string adUnitId)
        {
            Dummy();
        }

        public void DestroyRewardBasedVideoAd()
        {
            Dummy();
        }

        public void ShowRewardBasedVideoAd()
        {
            Dummy();
        }

        public void CreateAdLoader(AdLoader.Builder builder)
        {
            Dummy();
        }

        public void Load(AdRequest request)
        {
            Dummy();
        }

        public void SetAdSize(AdSize adSize)
        {
            Dummy();
        }

        public string MediationAdapterClassName()
        {
            Dummy();
            return null;
        }

    }
}
