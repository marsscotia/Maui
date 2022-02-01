﻿using System.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using FluentAssertions;
using Xunit;

namespace CommunityToolkit.Maui.UnitTests.Alerts;

public class Toast_Tests : BaseTest
{
	readonly IToast toast = new Toast();

	public Toast_Tests()
	{
		Assert.IsAssignableFrom<IAlert>(toast);
	}

	[Fact]
	public void ToastMake_NewToastCreatedWithValidProperties()
	{
		var expectedToast = new Toast
		{
			Duration = ToastDuration.Long,
			Text = "Test"
		};

		var currentToast = Toast.Make(
			"Test",
			ToastDuration.Long);

		currentToast.Should().BeEquivalentTo(expectedToast);
	}

	[Fact]
	public async Task ToastShow_CancellationTokenCancelled_ReceiveException()
	{
		var cancellationTokenSource = new CancellationTokenSource();
		cancellationTokenSource.Cancel();
		await toast.Invoking(x => x.Show(cancellationTokenSource.Token)).Should().ThrowExactlyAsync<OperationCanceledException>();
		cancellationTokenSource.Dispose();
	}

	[Fact]
	public async Task ToastDismiss_CancellationTokenCancelled_ReceiveException()
	{
		var cancellationTokenSource = new CancellationTokenSource();
		cancellationTokenSource.Cancel();
		await toast.Invoking(x => x.Dismiss(cancellationTokenSource.Token)).Should().ThrowExactlyAsync<OperationCanceledException>();
		cancellationTokenSource.Dispose();
	}

	[Fact]
	public async Task ToastShow_CancellationTokenNotCancelled_NotReceiveException()
	{
		var cancellationTokenSource = new CancellationTokenSource();
		await toast.Invoking(x => x.Show(cancellationTokenSource.Token)).Should().NotThrowAsync<OperationCanceledException>();
		cancellationTokenSource.Dispose();
	}

	[Fact]
	public async Task ToastDismiss_CancellationTokenNotCancelled_NotReceiveException()
	{
		var cancellationTokenSource = new CancellationTokenSource();
		await toast.Invoking(x => x.Dismiss(cancellationTokenSource.Token)).Should().NotThrowAsync<OperationCanceledException>();
		cancellationTokenSource.Dispose();
	}

	[Fact]
	public async Task ToastShow_CancellationTokenNone_NotReceiveException()
	{
		await toast.Invoking(x => x.Show(CancellationToken.None)).Should().NotThrowAsync<OperationCanceledException>();
	}

	[Fact]
	public async Task ToastDismiss_CancellationTokenNone_NotReceiveException()
	{
		await toast.Invoking(x => x.Dismiss(CancellationToken.None)).Should().NotThrowAsync<OperationCanceledException>();
	}

	[Fact]
	public void ToastMake_NewToastCreatedWithDefaultValues()
	{
		toast.Text.Should().BeEmpty();
		toast.Duration.Should().Be(ToastDuration.Short);
	}

	[Fact]
	public void ToastMake_NewToastCreatedWithNullString_ShouldThrowArgumentNullException()
	{
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(2)]
	[InlineData(int.MaxValue)]
	public void ToastMake_NewToastCreatedWithInvalidToastDuration_ShouldThrowInvalidEnumArgumentException(int duration)
	{
		Assert.Throws<InvalidEnumArgumentException>(() => Toast.Make("Invalid Duration", (ToastDuration)duration));
	}
}