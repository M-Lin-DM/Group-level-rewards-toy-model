%%
t1 = 100:100:29900;
kernelL = 20;
figure(2)
maTg = [smooth(Tg_RTg(:,2), kernelL), smooth(Tg_RTg(:,3), kernelL)];
plot(t1, maTg(:,1), '-', 'linewidth', 2); hold on

maTp = [smooth(Tp_RTg(:,2), kernelL), smooth(Tp_RTg(:,3), kernelL)];
plot(t1, maTp(0+(1:length(t1)),1), '.', 'linewidth', 2); 

maTpTg = [smooth(TpTg_RTg(:,2), kernelL), smooth(TpTg_RTg(:,3), kernelL)];
plot(t1, maTpTg(1:length(t1),1), '--', 'linewidth', 2); 

ma_untrained = [smooth(untrained(:,2), kernelL), smooth(untrained(:,3), kernelL)];
plot(t1, ma_untrained(1:length(t1),1), '-', 'linewidth', 2); 
xlabel('time steps', 'fontsize', 14); ylabel('group mean $T_g$', 'fontsize', 14, 'interpreter', 'latex'); 
legend('observation: $T_g$', 'observation: $T_p$', 'observation: $[T_p, T_g]$', 'observation: $T_p$ (untrained)', 'fontsize', 14, 'interpreter', 'latex')
%%
plot(t1, maTg(:,2), '-', 'linewidth', 2); hold on
plot(t1, maTp(0+(1:length(t1)), 2), '.', 'linewidth', 2); 
plot(t1, maTpTg(1:length(t1),2), '--', 'linewidth', 2); 
plot(t1, ma_untrained(300+(1:length(t1)), 2), '-', 'linewidth', 2); 

xlabel('time steps', 'fontsize', 14); ylabel('group standard deviation of $T_p$', 'fontsize', 14, 'interpreter', 'latex'); 
legend('observation: $T_g$', 'observation: $T_p$', 'observation: $[T_p, T_g]$', 'observation: $T_p$ (untrained)', 'fontsize', 14, 'interpreter', 'latex')